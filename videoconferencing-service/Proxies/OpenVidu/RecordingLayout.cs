namespace videoconferencing_service.Proxies.OpenVidu
{
    public enum RecordingLayout {

        /**
	 * All the videos are evenly distributed, taking up as much space as possible
	 */
        BestFit,

        /**
	 * <i>(not available yet)</i>
	 */
        PictureInPicture,

        /**
	 * <i>(not available yet)</i>
	 */
        VerticalPresentation,

        /**
	 * <i>(not available yet)</i>
	 */
        HorizontalPresentation,

        /**
	 * Use your own custom recording layout. See
	 * <a href="https://docs.openvidu.io/en/stable/advanced-features/recording#custom-recording-layouts"
	 * target="_blank">Custom recording layouts</a> to learn more
	 */
        Custom
    }
}