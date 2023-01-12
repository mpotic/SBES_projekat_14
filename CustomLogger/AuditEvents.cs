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
		CardCreationSuccess = 0,
		CardCreationFailure,
		ReplicationSuccess,
		ReplicationFailure,
		PinReplicationSuccess, 
		PinReplicationFailure, 
		PinChangeSuccess,
		PinChangeFailure,
		PaymentSuccess,
		PaymentFailure,
		PayoutSuccess,
		PayoutFailure
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

		public static string CardCreationSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.CardCreationSuccess.ToString());
			}
		}

		public static string CardCreationFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.CardCreationFailure.ToString());
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

		public static string PaymentSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PaymentSuccess.ToString());
			}
		}
		public static string PaymentFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PaymentFailure.ToString());
			}
		}
		public static string PayoutSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PayoutSuccess.ToString());
			}
		}

		public static string PayoutFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.PayoutFailure.ToString());
			}
		}
	}
}
