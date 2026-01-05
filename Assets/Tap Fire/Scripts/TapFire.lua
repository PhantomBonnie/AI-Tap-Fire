-- Register the behaviour
behaviour("TapFire")

function TapFire:Start()
	-- Run when behaviour is created
	print("START!")
end

function TapFire:Awake()
	-- Get our config values.
	self.Exclusion = self.script.mutator.GetConfigurationString("exclusion")
	self.Burst = self.script.mutator.GetConfigurationString("burst")
	self.CQBDistance = math.max(1, self.script.mutator.GetConfigurationInt("cqbDistance")) -- Minimum CQBDistance of 1, strictly enforced.
	self.MinGain = self.script.mutator.GetConfigurationFloat("minGain")
	self.Gain = self.script.mutator.GetConfigurationFloat("gain")
	self.Debug = self.script.mutator.GetConfigurationBool("debug")

	-- Get the list of blacklisted weapons by parsing the Exclusion string.
	-- The ; character will be used as a delimiter.

	if self.Debug then print("Parsing blacklist.") end
	self.Blacklist = self:ParseString(self.Exclusion)


	-- Get the list of burst fire weapons.
	if self.Debug then print("Parsing burst fire list.") end
	self.BurstFires = self:ParseString(self.Burst)

	GameEvents.onActorCreated.AddListener(self, "RegisterWeaponMonitor")

	for a, actor in ipairs(ActorManager.actors) do
		self:RegisterWeaponMonitor(actor)
	end
end

function TapFire:ParseString(toParse)
	local new = {}
	for str in string.gmatch(toParse, "[^;]+") do
		new[str] = str
		if self.Debug then print("Adding weapon with name [" .. str .. "] to the list.") end
	end

	return new
end

function TapFire:RegisterWeaponMonitor(actor)
	if actor and actor.isBot then
		self.script.AddValueMonitor("GetActorTargetDistance", "ApplyCorrectLogic", actor)
		if self.Debug then print("Adding monitor for actor.", actor, actor.name) end
	end
end

-- Get the distance between the actor and its target. If there is no target, then distance
-- is -1.
function TapFire:GetActorTargetDistance()
	local distance = -1
	local actor = CurrentEvent.listenerData
	if actor then
		local controller = actor.aiController

		if controller then
			local target = controller.currentAttackTarget
			if target then distance = ActorManager.ActorsDistance(actor, target) end
		end
	end

	return distance
end

-- Apply either the ammo conservation logic or burst fire logic, based on the weapon.
function TapFire:ApplyCorrectLogic(distance)
	local actor = CurrentEvent.listenerData

	-- Ensure the weapon is not nil/blacklisted and check to see if the weapon is either
	-- an assault rifle, battle rifle, or handgun, then apply a firing pattern logic.
	if actor and not (actor.isSeated and actor.activeSeat.hasActiveWeapon) then
		local w = actor.activeWeapon

		if w and w.weaponEntry and not self:StringIsInList(w.weaponEntry.name, self.Blacklist) then
			local role = w.GenerateWeaponRoleFromStats()
			if role and (role == WeaponRole.AutoRifle or role == WeaponRole.SemiAutoRifle
					or role == WeaponRole.Handgun or role == WeaponRole.Sniper or role == WeaponRole.Shotgun
					or role == WeaponRole.GrenadeLauncher or role == WeaponRole.Grenade) then
				-- Call the appropriate function to apply the reight logic, passing the actor's current weapon
				-- and the recorded distance to target as arguments.
				if not self:StringIsInList(w.weaponEntry.name, self.BurstFires) then
					self:ApplyConservationLogic(w, distance)
				else
					self:ApplyBurstLogic(w, distance)
				end
			end
		end
	end
end

function TapFire:StringIsInList(string, list)
	if list[string] then return true
	else return false end
end

-- This function will apply the ammo conservation logic given an actor and its active weapon.
function TapFire:ApplyConservationLogic(w, distance)
	-- Ensure the actor is not the player. If the actor is a bot, apply the ammo conservation logic.
	-- Otherwise, ensure that the active weapon does not gain heat when firing.
	if w then
		if self.Debug then print("Distance to target is: " .. distance) end

		-- If within the CQB Distance, don't apply heat to the weapon (letting it shoot without interruption).
		if distance <= self.CQBDistance then
			w.applyHeat = false
			if self.Debug then print("AHH he's right there shoot him shoot him!!!") end

			-- Beyond the CQB Distance, apply heat. The heat gain per shot starts at MinGain and increases by Gain for
			-- each meter past the CQB Distance multiplied by a random amount from 0.5 to 1.0.
			-- The heat drain rate per second starts at 20 * MinGain + heatGainPerShot, and is subtracted by Gain for
			-- each meter past the CQB Distance multiplied by a random amount from 0.5 to 1.0.
			-- The math.max() method is to ensure that either value is at least 0.01 (to avoid negative numbers, which can cause
			-- some unintended behavior).
		else
			w.applyHeat = true
			w.heatGainPerShot = math.max(0.01,
				self.MinGain + ((self.Gain * (distance - self.CQBDistance)) * math.random(1.0, 3.0)))
			w.heatDrainRate = math.max(0.01,
				(20 * self.MinGain) + w.heatGainPerShot -
				((self.Gain * (distance - self.CQBDistance)) * math.random(1.0, 3.0)))

			if self.Debug then
				print("Weapon gains " .. w.heatGainPerShot .. " per shot. Recovers "
					.. w.heatDrainRate .. " per second. Current heat is: " .. w.heat)
			end
		end
	end
end

function TapFire:ApplyBurstLogic(w, distance)
	-- Ensure the actor is not the player. If the actor is a bot, apply the ammo burst logic.
	-- Otherwise, ensure that the active weapon does not gain heat when firing.
	if w then
		if self.Debug then print("Applying burst logic.") end

		if self.Debug then print("Distance to target is: " .. distance) end

		-- If within the CQB Distance, don't apply heat to the weapon (letting it shoot without interruption).
		if distance <= self.CQBDistance then
			w.applyHeat = false
			if self.Debug then print("AHH he's right there shoot him shoot him!!!") end

			-- If beyond the CQB Distance, then apply a random amount of heat per shot so that the weapon fires in bursts.
			-- Set the heat drain rate to a random value so that the interval between bursts is random.
		else
			w.applyHeat = true
			w.heatGainPerShot = 0.15 * math.random(0.7, 1.4)
			w.heatDrainRate = 1 * math.random(0.5, 2)

			if self.Debug then
				print("Weapon gains " .. w.heatGainPerShot .. " per shot. Recovers "
					.. w.heatDrainRate .. " per second. Current heat is: " .. w.heat)
			end
		end
	end
end
