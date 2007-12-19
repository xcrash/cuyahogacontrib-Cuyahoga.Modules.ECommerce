using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

	/// <summary>
	/// Summary description for CompositeCommand.
	/// </summary>
	public class CompositeOrderProcessor : IOrderProcessor {
		
		public const string COMMAND_LIST_SEPERATOR = ";";

		private IList<IOrderProcessor> _processList;

		public CompositeOrderProcessor() {
			_processList = new List<IOrderProcessor>();
		}

        public CompositeOrderProcessor(IList<IOrderProcessor> processList) {
			_processList = processList;
		}

		#region IOrderProcessor Members

		public void Process(IBasket order) {
			for (int i = 0; i < _processList.Count; i++) {
				(_processList[i]).Process(order);
			}		
		}
		#endregion

		public void Add(IOrderProcessor processor) {
			_processList.Add(processor);
		}

		public void Add(IList<IOrderProcessor> processList) {
			for (int i = 0; i < processList.Count; i++) {
				Add(processList[i]);
			}
		}

		public void Remove(IOrderProcessor processor) {
			_processList.Remove(processor);
		}

		public void Remove(IList<IOrderProcessor> processList) {
			for (int i = 0; i < processList.Count; i++) {
				Remove(processList[i]);
			}
		}
	}
}