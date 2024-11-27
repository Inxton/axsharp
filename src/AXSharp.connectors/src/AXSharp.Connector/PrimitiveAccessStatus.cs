// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Connector
{
    /// <summary>
    /// Provides information about access to a primitive item.
    /// </summary>
    public class PrimitiveAccessStatus : INotifyPropertyChanged
    {
        private bool _failure;

        /// <summary>
        /// Updates information about the access to the respective variable.
        /// </summary>
        /// <param name="cycle">Connector cycle (not to be confuses with PLC cycle)</param>
        /// <param name="failureReason">Failure reason description. No failure if empty.</param>
        public void Update(long cycle, string failureReason = "")
        {
            this.Cycle = cycle;
            this.LastAccess = DateTime.Now;
            this.FailureReason = failureReason;
            this.Failure = !string.IsNullOrEmpty(FailureReason);
        }

        /// <summary>
        /// Gets or sets the last cycle counter in which the item was accessed.
        /// </summary>
        public long Cycle { get; set; }

        /// <summary>
        /// Gets or sets time stamp of the last access to the item.
        /// </summary>
        public DateTime LastAccess { get; set; }

        /// <summary>
        /// Gets or sets whether there was a failure to access the item.
        /// Has notify property changed, can be used to indicate a problem in the UI.
        /// </summary>
        public bool Failure
        {
            get => _failure;
            set => SetField(ref _failure, value);
        }

        /// <summary>
        /// Gets or sets the reason of access failure.
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the event when a property value changes.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the field when the incoming value changes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
