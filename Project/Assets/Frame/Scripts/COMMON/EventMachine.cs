using TOOL;

namespace GameCommon{

	/// <summary>
	/// 事件管理器
	/// </summary>
	public class EventMachine : IEventMachine
	{
		private static EventMachine instance = null;
		public static EventMachine gInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new EventMachine();
				}
				
				return instance;
			}
		}

        public static void SetNull(){
            instance = null;
        }

		/// <summary>
		/// 注册事件
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="function">Function.</param>
		public static void Register(EventID key,OnEvent function){
			gInstance.Add(key,function);
		}

		/// <summary>
		/// 注销事件
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="function">Function.</param>
		public static void Unregister(EventID key,OnEvent function){
			gInstance.Remove(key,function);
		}

		/// <summary>
		/// 发送事件
		/// </summary>
		/// <param name="key">Key.</param>
		public static void SendEvent(EventID key,params object[] args){
			gInstance.Send(key,args);
		}

	}

	//EventMachine.SendEvent(EventID.Title);
	//EventMachine.Register(EventID.Title, OnGamePrepare);
	//EventMachine.Unregister(EventID.Title, OnGamePrepare);
	//void OnChangeUI(params object[] args)
}