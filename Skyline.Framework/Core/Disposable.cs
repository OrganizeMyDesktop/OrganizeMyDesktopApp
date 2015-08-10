namespace Skyline.Framework.Core
{
	using System;
	abstract public class Disposable : IDisposable
	{
		public bool IsDisposed { get; private set; }
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Disposable()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					DisposeManagedResources();
				}

				DisposeUnmanagedResources();
				IsDisposed = true;
			}
		}

		protected virtual void DisposeManagedResources() { }
		protected virtual void DisposeUnmanagedResources() { }
	}
}
