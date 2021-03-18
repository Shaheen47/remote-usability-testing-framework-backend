using System;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public class RecordingProperties
    {
        private String name;
        private Recording.OutputMode outputMode;
        private RecordingLayout recordingLayout;
        private String customLayout;
        private String resolution;
        private bool hasAudio;
        private bool hasVideo;
        private long shmSize; // For COMPOSED recording
        private String mediaNode;
    }
}