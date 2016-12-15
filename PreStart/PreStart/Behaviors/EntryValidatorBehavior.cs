using Xamarin.Forms;

namespace PreStart.Behaviors
{
    public class EntryValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry) sender);
            bool isValid = entry.Text.Length > 0;
            entry.BackgroundColor = isValid ? Color.Default : Color.Red;
        }
    }
}
