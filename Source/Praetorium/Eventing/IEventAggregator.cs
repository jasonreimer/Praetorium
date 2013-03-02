namespace Praetorium.Eventing
{
    public interface IEventAggregator
    {

        /// <summary>
        /// Sends the specified event instance to all subscribers of <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        IEventAggregator Send<TEvent>(TEvent e) where TEvent : class;

        /// <summary>
        /// Registers the listener by subscribing to all events requested by the listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        /// <remarks>
        /// <para>
        /// In order for any subscriptions to be created, the <paramref name="listener"/> must
        /// implement at least one form of <see cref="IListener&lt;T&gt;"/>.
        /// </para>
        /// </remarks>
        IEventAggregator RegisterListener(object listener);

        /// <summary>
        /// Unregisters the listener by removing an event subscriptions for the listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        IEventAggregator UnregisterListener(object listener);

    }
}
