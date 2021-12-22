﻿namespace lightstudio
{
  /// <summary>
  ///   Describes an item that can be placed on a track in the timeline.
  /// </summary>
  public interface ITimelineTrack : ITimelineTrackBase {
    /// <summary>
    ///   The beginning of the item.
    /// </summary>
    float Start { get; set; }

    /// <summary>
    ///   The end of the item.
    /// </summary>
    float End { get; set; }
  }
}