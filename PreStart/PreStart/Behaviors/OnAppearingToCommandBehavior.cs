using System.Windows.Input;
using Xamarin.Forms;

namespace PreStart.Behaviors
{
    class OnAppearingToCommandBehavior : Behavior<ContentPage>
    {
        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create("Command", typeof(ICommand), typeof(OnAppearingToCommandBehavior));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Appearing += Bindable_Appearing;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Appearing -= Bindable_Appearing;
        }

        private void Bindable_Appearing(object sender, System.EventArgs e)
        {
            if (Command == null)
            {
                return;
            }
            Command.Execute(null);
        }
    }
}
