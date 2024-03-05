using System.Text;

public static class Debugger
{
	public static void Debug(string data)
	{
		Log log = new();

		log.Debug(data);

		log.Dispose();
	}
}

public class Log : IDisposable
{
	private bool disposed = false;
	readonly StreamWriter debug;

	public Log()
	{
		debug = CreateFile($"logs/{DateTime.Now.ToLocalTime().ToShortDateString().Replace('/', '-')}.log");

	}

	public Log(string name)
	{
		debug = CreateFile($"logs/{name}.log");
	}

	~Log()
	{
		Dispose(disposing: false);
	}

	private StreamWriter CreateFile(string path)
	{
		Directory.CreateDirectory("./logs");
		if (File.Exists(path))
			return File.AppendText(path);

		File.Create(path).Close();
		return File.AppendText(path);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposed) return;

		if (disposing)
			debug.Close();

		disposed = true;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this); // Suppress finalization (no need for finalizer)
	}

	public void Debug(string log)
	{
		debug.Write(log);
	}
}
