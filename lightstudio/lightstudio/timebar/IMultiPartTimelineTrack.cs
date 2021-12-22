using System.Collections.Generic;

namespace lightstudio
{
  /// <summary>
  ///   A timeline track that contains multiple parts.
  /// </summary>
  public interface IMultiPartTimelineTrack : ITimelineTrackBase {
    /// <summary>
    ///   The elements within this track.
    /// </summary>
    IEnumerable<ITimelineTrack> TrackElements { get; }
  }
}