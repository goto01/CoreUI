namespace Staff
{
	public delegate void EventHandler<T>(T sender);
	
	public static class EventHandlerExtended
	{
		public static void Raise<T>(this EventHandler<T> @event, T sender)
		{
			var handler = @event;
			if (handler != null) handler.Invoke(sender);
		}
	}
}