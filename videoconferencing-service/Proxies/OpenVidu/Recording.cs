using Newtonsoft.Json;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public class Recording
    {
        public enum Status {

		/**
		 * The recording is starting (cannot be stopped). Some recording may not go
		 * through this status and directly reach "started" status
		 */
		Starting,

		/**
		 * The recording has started and is going on
		 */
		Started,

		/**
		 * The recording has stopped and is being processed. At some point it will reach
		 * "ready" status
		 */
		Stopped,

		/**
		 * The recording has finished being processed and is available for download
		 * through property {@link Recording#getUrl}
		 */
		Ready,

		/**
		 * The recording has failed. This status may be reached from "starting",
		 * "started" and "stopped" status
		 */
		Failed
	}

	/**
	 * See {@link io.openvidu.java.client.Recording#getOutputMode()}
	 */
	public enum OutputMode {

		/**
		 * Record all streams in a grid layout in a single archive
		 */
		Composed,

		/**
		 * Record each stream individually
		 */
		Individual,

		/**
		 * Works the same way as COMPOSED mode, but the necessary recorder service
		 * module will start some time in advance and won't be terminated once a
		 * specific session recording has ended. This module will remain up and running
		 * as long as the session remains active.<br>
		 * <br>
		 * 
		 * <ul>
		 * <li><strong>Pros vs COMPOSED</strong>: the process of starting the recording
		 * will be noticeably faster. This can be very useful in use cases where a
		 * session needs to be recorded multiple times over time, when a better response
		 * time is usually desirable.</li>
		 * <li><strong>Cons vs COMPOSED</strong>: for every session initialized with
		 * COMPOSED_QUICK_START recording output mode, extra CPU power will be required
		 * in OpenVidu Server. The recording module will be continuously rendering all
		 * of the streams being published to the session even when the session is not
		 * being recorded. And that is for every session configured with
		 * COMPOSED_QUICK_START.</li>
		 * </ul>
		 */
		ComposedQuickStart
	}

	private Recording.Status status;

	[JsonProperty(PropertyName = "id")]
	public string id { set; get; }
	
	[JsonProperty(PropertyName = "sessionId")]
	public string sessionId{ set; get; }
	
	[JsonProperty(PropertyName = "createdAt")]
	public long createdAt{ set; get; }
	
	[JsonProperty(PropertyName = "size")]
	public long size{ set; get; }
	
	[JsonProperty(PropertyName = "duration")]
	public double duration{ set; get; }
	
	[JsonProperty(PropertyName = "url")]
	public string url{ set; get; }
	
	[JsonProperty(PropertyName = "name")]
	public string name{ set; get; }
	/*public RecordingProperties recordingProperties;*/
    }
}