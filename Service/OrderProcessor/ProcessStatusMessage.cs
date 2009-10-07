using System;
using System.Collections.Generic;
using System.Text;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public enum ProcessStatus {
        NotKnown = 0,
        Success,
        Warning,
        Error
    }

    public class ProcessStatusMessage {

        private ProcessStatus _status;
        private string _message;

        public ProcessStatusMessage() {
        }

        public ProcessStatusMessage(ProcessStatus status) {
            _status = status;
        }

        public ProcessStatusMessage(ProcessStatus status, string message)
            : this(status) {
            _message = message;
        }

        public ProcessStatus Status {
            get { return _status; }
            set { _status = value; }
        }

        public string Message {
            get { return _message; }
            set { _message = value; }
        }
    }
}
