using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogger
{
	public enum AuditEventTypes
	{
		ReplicationSuccess = 0,
		ReplicationFailure,
		PinReplicationSuccess, 
		PinReplicationFailure, 
		PinChangeSuccess,
		PinChangeFailure
	}

	public class AuditEvents
	{
		private static ResourceManager resourceManager = null;
		private static object resourceLock = new object();

		private static ResourceManager ResourceMgr
		{
			get
			{
				lock (resourceLock)
				{
					if (resourceManager == null)
					{
						resourceManager = new ResourceManager
							(typeof(AuditEventFile).ToString(),
							Assembly.GetExecutingAssembly());
					}
					return resourceManager;
				}
			}
		}

		public static string ReplicationSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.ReplicationSuccess.ToString());
			}
		}

		public static string ReplicationFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.ReplicationFailure.ToString());
			}
		}
		public static string PinReplicationSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PinReplicationSuccess.ToString());
			}
		}

		public static string PinReplicationFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PinReplicationFailure.ToString());
			}
		}

		public static string PinChangeSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PinChangeSuccess.ToString());
			}
		}

		public static string PinChangeFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PinChangeFailure.ToString());
			}
		}

	}
}
