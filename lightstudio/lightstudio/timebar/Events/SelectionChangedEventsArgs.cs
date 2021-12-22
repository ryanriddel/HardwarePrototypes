﻿using System;
using System.Collections.Generic;

namespace lightstudio.Events {
  /// <summary>
  ///   Event arguments for an event that notifies about a change in the selection
  ///   of tracks.
  /// </summary>
  public class SelectionChangedEventArgs : EventArgs {
    /// <summary>
    ///   The tracks that were selected in the operation.
    /// </summary>
    public IEnumerable<ITimelineTrackBase> Selected { get; private set; }

    /// <summary>
    ///   The track elements that were deselected in the operation.
    /// </summary>
    public IEnumerable<ITimelineTrackBase> Deselected { get; private set; }

    /// <summary>
    ///   Construct a new SelectionChangedEventArgs instance.
    /// </summary>
    /// <param name="selected">The track elements that were deselected in the operation.</param>
    /// <param name="deselected">The tracks that were selected in the operation.</param>
    public SelectionChangedEventArgs( IEnumerable<ITimelineTrackBase> selected, IEnumerable<ITimelineTrackBase> deselected ) {
      Selected = selected;
      Deselected = deselected;
    }

    /// <summary>
    ///   An empty instance of the <see cref="SelectionChangedEventArgs"/> class.
    /// </summary>
    public new static SelectionChangedEventArgs Empty {
      get { return new SelectionChangedEventArgs( null, null ); }
    }
  }
}