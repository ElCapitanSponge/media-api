using DotEnv.Core;

namespace media_api.Middleware;

public class EnvMiddleware
{
	#region Methods
	public EnvMiddleware()
	{
		new EnvLoader().Load();
		this._Reader = new EnvReader();
	}

	public EnvReader reader
	{
		get
		{
			return this._Reader;
		}
	}
	#endregion

	#region Params
	private EnvReader _Reader;
	#endregion

}
