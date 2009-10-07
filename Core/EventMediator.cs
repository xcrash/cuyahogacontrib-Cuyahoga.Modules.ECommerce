using System;
using System.Collections;
using System.Web;

namespace Cuyahoga.Modules.ECommerce.Core {

	public delegate void MediatorEventHandler(object source, MediatorEventArgs args);

	public class MediatorEventArgs : EventArgs {

		public readonly string EventName;
		public Hashtable Properties;

		public MediatorEventArgs(string eventName) {
			EventName = eventName;
			Properties = new Hashtable();
		}
	}

	/// <summary>
	/// Allows all modules and controls to be notified of other events occuring in a request
	/// </summary>
	public class EventMediator {

		public const string ITEM_NAME = "mediator";
		private HttpContext _context;


		private EventMediator(HttpContext context) {
			_context = context;
		}

		public static EventMediator Instance {
			get {
				
				HttpContext context = HttpContext.Current;
				EventMediator mediator = (EventMediator) context.Items[ITEM_NAME] as EventMediator;

				if (mediator == null) {
					mediator = new EventMediator(context);
					context.Items[ITEM_NAME] = mediator;
				}

				return mediator;
			}
		}

		public event MediatorEventHandler EventRaised;

		/// <summary>
		/// Allows external objects to raise an event within this page
		/// </summary>
		/// <param name="source"><c>object</c> raising the event</param>
		/// <param name="args"><c>Hashtable</c> of data supplied by the object</param>
		/// <remarks>Uses the Mediator pattern to decouple external objects from their listeners</remarks>
		public void Notify(object source, MediatorEventArgs args) {
			if (EventRaised != null) {
				EventRaised(source, args);
			}
		}
	}
}