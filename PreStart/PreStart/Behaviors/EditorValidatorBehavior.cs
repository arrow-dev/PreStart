using System;
using Xamarin.Forms;

namespace Prestart.Behaviors
{
    public class EditorValidatorBehavior : Behavior<Editor>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid",
                typeof(bool),
                typeof(EditorValidatorBehavior),
                false);

        public static readonly BindableProperty IsValidProperty =
            IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            private set { SetValue(IsValidPropertyKey, value); }
            get { return (bool)GetValue(IsValidProperty); }
        }

        protected override void OnAttachedTo(Editor editor)
        {
            editor.TextChanged += Editor_TextChanged;
            base.OnAttachedTo(editor);
        }

        protected override void OnDetachingFrom(Editor editor)
        {
            editor.TextChanged -= Editor_TextChanged;
            base.OnDetachingFrom(editor);
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var editor = ((Editor)sender);
            IsValid = IsValideditor(editor.Text);
        }

        bool IsValideditor(string editorText)
        {
            if (String.IsNullOrEmpty(editorText) | String.IsNullOrWhiteSpace(editorText))
            {
                return false;
            }

            return true;
        }
    }
}
