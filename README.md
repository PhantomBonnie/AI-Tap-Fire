# AI-Tap-Fire
### Steam Workshop link: https://steamcommunity.com/sharedfiles/filedetails/?id=3495202789.
### Description from mod page: 
This mod will introduce the concept of ammo conservation to Ravenfield's beloved AI soldiers! No longer will bots mindlessly spray their weapon on full auto at targets hundreds of meters away. Instead, they will take slow, aimed shots or fire in short bursts depending on the distance, and as they get closer to the enemy they will also fire more aggressively.

The mod accomplishes this by manipulating the heat mechanic for weapons, forcing a bot's weapon to overheat when they engage targets at extreme distances (and thereby slowing their rate of fire).
This mod only affects small arms. Vehicle weapons (even those that are automatic) are not affected and will continue to rapid fire at their targets. Only bot weapons are affected.
Because this mutator utilizes Ravenfield's weapon overheating mechanic, it is recommended that you exclude any weapons that have a built in overheat system to avoid potential issues.

You can configure the following values:
CQB Distance (Default 30 meters)
- When a bot engages a target within this distance, their weapon will not gain heat and they will fire full auto as usual.

Minimum Heat Gain (Default 0.1)
- The minimum amount of heat added to a weapon when firing (assuming the target is beyond the CQB distance).

Heat Gain Factor (Default 0.01)
- An important number that determines the amount of extra heat that gets added to the weapon for each meter the target is beyond the CQB Distance.

Burst Fire List
- Any weapon names added to this list will not follow the ammo conservation logic described below. Instead, they will follow a different logic where they fire in controlled bursts of varying length.
- This logic is perfect for weapons that you want to fire in a more aggressive but controlled manner, such as LMGs and certain SMGs. Don't forget your delimiters.

How this mod works:
- When a bot engages a target beyond the CQB Distance, its weapon gains heat.
- When its heat value reaches 1, the weapon cannot fire until heat reaches 0.
- When engaging a target within the CQB Distance, no heat is added.
- Heat Gain Factor determines how much heat a weapon gains for each meter the target is beyond the CQB Distance.
- This value is added with the Minimum Heat Gain to calculate heat gain per shot.
- A bit of RNG is added to make firing patterns less predictable.
- Heat loss per second is calculated with the Heat Gain Factor, heat gain per second, target distance, and RNG.

Overall, higher Heat Gain Factor results in shorter bursts of fire as distance increases.
This mod is perfect for those who want a more realistic infantry fighting experience. It pairs well with mods that change gunplay, such as Universal Recoil, which can force the player to also control their shots.
