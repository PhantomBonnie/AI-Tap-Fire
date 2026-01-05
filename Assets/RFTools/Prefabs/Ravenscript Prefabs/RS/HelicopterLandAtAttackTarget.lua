behaviour("HelicopterLandAtAttackTarget")

function HelicopterLandAtAttackTarget:Awake()
	self.vehicle = self.gameObject.GetComponentInParent(Helicopter)

	if(self.vehicle == nil) then
		print("Could not find helicopter component :(")
		return
	end

	self.vehicle.onClaimedBySquad.AddListener(self, "OnClaim")
	self.vehicle.onClaimDropped.AddListener(self, "OnClaimDrop")
end

function HelicopterLandAtAttackTarget:OnClaim(squad)
	self.squad = squad
	squad.onIssueOrderMovement.AddListener(self, "OnIssueMovement")
end

function HelicopterLandAtAttackTarget:OnClaimDrop(squad)
	squad.onIssueOrderMovement.RemoveListener(self, "OnIssueMovement")
	squad.autoDropTransportedPassengers = true
end

function HelicopterLandAtAttackTarget:OnIssueMovement(order)

	-- By default, use the automatic dropping strategy
	self.squad.autoDropTransportedPassengers = true

	-- We only change the strategy on attack orders
	if order.type ~= OrderType.Attack then
		return
	end

	-- Ensure squad leader is driving the vehicle
	if not self.squad.leader.isDriver then
		return
	end

	local targetPosition = order.targetPoint.transform.position
	
	if order.hasOverrideTargetPosition then
		targetPosition = order.GetOverrideTargetPosition()
	end

	local lz = HelicopterLandingZone.GetClosestUnclaimed(targetPosition)

	self.squad.autoDropTransportedPassengers = false
	self.squad.LandHelicopterAndClaimLandingZone(lz, "OnLand")
end

function HelicopterLandAtAttackTarget:OnLand()
	self.squad.DropTransportedPassengers()
	self.squad.TakeOff()
	self.squad.ReleaseLandingZoneClaim()
end