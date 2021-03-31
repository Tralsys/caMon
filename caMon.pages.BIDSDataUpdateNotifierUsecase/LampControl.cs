using System.Windows;
using System.Windows.Controls;

using BIDSDataUpdateNotifier;

namespace caMon.pages.BIDSDataUpdateNotifierUsecase
{
	public class LampControl : Control
	{
		static public DependencyProperty ValueCheckerProperty = DependencyProperty.Register(nameof(ValueChecker), typeof(IValueChecker<bool>), typeof(LampControl));
		public BoolValueProvider ValueChecker { get => GetValue(ValueCheckerProperty) as BoolValueProvider; set => SetValue(ValueCheckerProperty, value); }
		
		static public DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(LampControl));
		public object Content { get => GetValue(ContentProperty); set => SetValue(ContentProperty, value); }

		static LampControl() => DefaultStyleKeyProperty.OverrideMetadata(typeof(LampControl), new FrameworkPropertyMetadata(typeof(LampControl)));
		
	}
}
