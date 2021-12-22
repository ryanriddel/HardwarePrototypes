using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using lightstudio;

namespace lightstudio.TestObjects {
  class AdjustMyLength : ITimelineTrack {
    public float Start { get; set; }
    public float End { get; set; }
    public string Name { get; set; }

    public override string ToString() {
      return string.Format( "Name: {0}, End: {1}, Start: {2}", Name, End, Start );
    }
  }
}
