using System;

namespace Ravenfield.Trigger
{
	public class SignalDocAttribute : Attribute
	{
		public SignalDocAttribute(string doc) {
			this.doc = doc;
		}

		public ContextSource contextSource;
		public string doc, actor, vehicle, squad, weapon, overrideName;

		// Signal source signifies which TriggerSignal construcor is used to construct the signal context.
		// Specific constructors will auto-populate some fields, so this value is used for inspector signal documentation of auto-populated fields.
		public enum ContextSource
		{
			None,
			Actor,
			Squad,
			Vehicle,
		}

		public string GetActorDocString() {
			return this.actor;
		}

		public string GetVehicleDocString() {
			if(!string.IsNullOrEmpty(this.vehicle)) {
				return this.vehicle;
			}

			switch(this.contextSource) {
				case ContextSource.Actor:
					return "The vehicle the actor is inside";

				case ContextSource.Squad:
					return "The squad's assigned vehicle";
			}

			return string.Empty;
		}

		public string GetSquadDocString() {
			if (!string.IsNullOrEmpty(this.squad)) {
				return this.squad;
			}

			switch (this.contextSource) {
				case ContextSource.Actor:
					return "The actor's squad";

				case ContextSource.Vehicle:
					return "The squad that claims this vehicle";
			}

			return string.Empty;
		}

		public string GetWeaponDocString() {
			return this.weapon;
		}
	}
}