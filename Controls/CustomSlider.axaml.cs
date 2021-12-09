using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AvaloniaBoundsIssue.Controls
{
	public class CustomSlider : RangeBase
	{
		private Border? _indicator;

		static CustomSlider()
		{
			ValueProperty.Changed.AddClassHandler<CustomSlider>((x, e) => x.UpdateIndicatorWhenPropChanged(e));
			MinimumProperty.Changed.AddClassHandler<CustomSlider>((x, e) => x.UpdateIndicatorWhenPropChanged(e));
			MaximumProperty.Changed.AddClassHandler<CustomSlider>((x, e) => x.UpdateIndicatorWhenPropChanged(e));
			// Janky(?) alternative
			// BoundsProperty.Changed.AddClassHandler<CustomSlider>((x, e) => x.UpdateIndicatorWhenPropChanged(e));
		}

		public CustomSlider()
		{
			
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			UpdateIndicator(finalSize);
			return base.ArrangeOverride(finalSize);
		}

		protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
		{
			_indicator = e.NameScope.Get<Border>("PART_Indicator");

			UpdateIndicator(Bounds.Size);
		}

		private void UpdateIndicator(Size bounds)
		{
			if (_indicator != null)
			{
				double percent = Maximum == Minimum ? 1.0 : (Value - Minimum) / (Maximum - Minimum);
				// bounds reports all-0 entry at startup.
				_indicator.Width = bounds.Width * percent;
			}
		}

		private void UpdateIndicatorWhenPropChanged(AvaloniaPropertyChangedEventArgs e)
		{
			UpdateIndicator(Bounds.Size);
		}
	}
}
