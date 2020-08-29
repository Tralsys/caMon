using System.ComponentModel;

namespace BIDSData_toBind
{
	public class OnPropChangedIncluded : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
	}
}
