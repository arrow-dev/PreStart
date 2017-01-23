using System;
using Xamarin.Forms;



namespace PreStart.Behaviors
{

    public class BehaviorsTest : Behavior<Editor>
    {
        //an IsValid property that is backed by a bindable property
        //it is a read-only bindable property.
        private static readonly BindablePropertyKey IsvalidPropertyKey =
            BindableProperty.CreateReadOnly("isValid",
                typeof(bool),
                typeof(BehaviorsTest),
                false);

        public static readonly BindableProperty IsValidProperty =
            IsvalidPropertyKey.BindableProperty;

        public bool IsValid
        {
            private set { SetValue(IsvalidPropertyKey, value); }
            get { return (bool)GetValue(IsValidProperty); }
        }

        protected override void OnAttachedTo(Editor entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);

        }
        protected override void OnDetachingFrom(Editor entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);

        }

        // Behavior implementation
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Editor editor = (Editor)sender;
            IsValid = IsValidForm(editor.Text);
        }

        bool IsValidForm(string str)
        {
            if (String.IsNullOrEmpty(str) | String.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            return true;

        }
    }

}