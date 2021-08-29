using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class Ano2018
    {
        public string label { get; set; }
        public List<double> data { get; set; }
        public string fill { get; set; }
    }

    public class Ano2019
    {
        public string label { get; set; }
        public List<double> data { get; set; }
        public string fill { get; set; }
    }

    public class Datasets
    {
        public List<Ano2018> ano2018 { get; set; }
        public List<Ano2019> ano2019 { get; set; }
    }

    public class Color
    {
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
        public string pointBackgroundColor { get; set; }
        public string pointHoverBackgroundColor { get; set; }
        public string pointBorderColor { get; set; }
        public string pointHoverBorderColor { get; set; }
    }

    public class Legend
    {
        public bool display { get; set; }
    }

    public class Padding
    {
        public int top { get; set; }
        public int left { get; set; }
        public int right { get; set; }
    }

    public class Layout
    {
        public Padding padding { get; set; }
    }

    public class Point
    {
        public int radius { get; set; }
        public int borderWidth { get; set; }
        public int hoverRadius { get; set; }
        public int hoverBorderWidth { get; set; }
    }

    public class Line
    {
        public int tension { get; set; }
    }

    public class Elements
    {
        public Point point { get; set; }
        public Line line { get; set; }
    }

    public class GridLines
    {
        public bool display { get; set; }
        public bool drawBorder { get; set; }
        public int tickMarkLength { get; set; }
    }

    public class Ticks
    {
        public string fontColor { get; set; }
    }

    public class XAx
    {
        public GridLines gridLines { get; set; }
        public Ticks ticks { get; set; }
    }

    public class Ticks2
    {
        public double min { get; set; }
        public int max { get; set; }
        public double stepSize { get; set; }
    }

    public class YAx
    {
        public bool display { get; set; }
        public Ticks2 ticks { get; set; }
    }

    public class Scales
    {
        public List<XAx> xAxes { get; set; }
        public List<YAx> yAxes { get; set; }
    }

    public class Filler
    {
        public bool propagate { get; set; }
    }

    public class XLabelsOnTop
    {
        public bool active { get; set; }
    }

    public class Plugins
    {
        public Filler filler { get; set; }
        public XLabelsOnTop xLabelsOnTop { get; set; }
    }

    public class Options
    {
        public bool spanGaps { get; set; }
        public Legend legend { get; set; }
        public bool maintainAspectRatio { get; set; }
        public Layout layout { get; set; }
        public Elements elements { get; set; }
        public Scales scales { get; set; }
        public Plugins plugins { get; set; }
    }

    public class Widget1
    {
        public string chartType { get; set; }
        public Datasets datasets { get; set; }
        public List<string> labels { get; set; }
        public List<Color> colors { get; set; }
        public Options options { get; set; }
    }

    public class Conversion
    {
        public int value { get; set; }
        public int ofTarget { get; set; }
    }

    public class Dataset
    {
        public string label { get; set; }
        public List<int> __invalid_name__data { get; set; }
    }

    public class Color2
    {
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
    }

    public class Legend2
    {
        public bool display { get; set; }
    }

    public class Padding2
    {
        public int top { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }
    }

    public class Layout2
    {
        public Padding2 padding { get; set; }
    }

    public class XAx2
    {
        public bool display { get; set; }
    }

    public class Ticks3
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class YAx2
    {
        public bool display { get; set; }
        public Ticks3 ticks { get; set; }
    }

    public class Scales2
    {
        public List<XAx2> xAxes { get; set; }
        public List<YAx2> yAxes { get; set; }
    }

    public class Options2
    {
        public bool spanGaps { get; set; }
        public Legend2 legend { get; set; }
        public bool maintainAspectRatio { get; set; }
        public Layout2 layout { get; set; }
        public Scales2 scales { get; set; }
    }

    public class Widget2
    {
        public Conversion conversion { get; set; }
        public string chartType { get; set; }
        public List<Dataset> datasets { get; set; }
        public List<string> labels { get; set; }
        public List<Color2> colors { get; set; }
        public Options2 options { get; set; }
    }

    public class Impressions
    {
        public string value { get; set; }
        public int ofTarget { get; set; }
    }

    public class Dataset2
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public bool fill { get; set; }
    }

    public class Color3
    {
        public string borderColor { get; set; }
    }

    public class Legend3
    {
        public bool display { get; set; }
    }

    public class Point2
    {
        public int radius { get; set; }
        public int borderWidth { get; set; }
        public int hoverRadius { get; set; }
        public int hoverBorderWidth { get; set; }
    }

    public class Line2
    {
        public int tension { get; set; }
    }

    public class Elements2
    {
        public Point2 point { get; set; }
        public Line2 line { get; set; }
    }

    public class Padding3
    {
        public int top { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }
    }

    public class Layout3
    {
        public Padding3 padding { get; set; }
    }

    public class XAx3
    {
        public bool display { get; set; }
    }

    public class Ticks4
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class YAx3
    {
        public bool display { get; set; }
        public Ticks4 ticks { get; set; }
    }

    public class Scales3
    {
        public List<XAx3> xAxes { get; set; }
        public List<YAx3> yAxes { get; set; }
    }

    public class Options3
    {
        public bool spanGaps { get; set; }
        public Legend3 legend { get; set; }
        public bool maintainAspectRatio { get; set; }
        public Elements2 elements { get; set; }
        public Layout3 layout { get; set; }
        public Scales3 scales { get; set; }
    }

    public class Widget3
    {
        public Impressions impressions { get; set; }
        public string chartType { get; set; }
        public List<Dataset2> datasets { get; set; }
        public List<string> labels { get; set; }
        public List<Color3> colors { get; set; }
        public Options3 options { get; set; }
    }

    public class Visits
    {
        public int value { get; set; }
        public int ofTarget { get; set; }
    }

    public class Dataset3
    {
        public string label { get; set; }
        public List<int> data { get; set; }
    }

    public class Color4
    {
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
    }

    public class Legend4
    {
        public bool display { get; set; }
    }

    public class Padding4
    {
        public int top { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }
    }

    public class Layout4
    {
        public Padding4 padding { get; set; }
    }

    public class XAx4
    {
        public bool display { get; set; }
    }

    public class Ticks5
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class YAx4
    {
        public bool display { get; set; }
        public Ticks5 ticks { get; set; }
    }

    public class Scales4
    {
        public List<XAx4> xAxes { get; set; }
        public List<YAx4> yAxes { get; set; }
    }

    public class Options4
    {
        public bool spanGaps { get; set; }
        public Legend4 legend { get; set; }
        public bool maintainAspectRatio { get; set; }
        public Layout4 layout { get; set; }
        public Scales4 scales { get; set; }
    }

    public class Widget4
    {
        public Visits visits { get; set; }
        public string chartType { get; set; }
        public List<Dataset3> datasets { get; set; }
        public List<string> labels { get; set; }
        public List<Color4> colors { get; set; }
        public Options4 options { get; set; }
    }

    public class Yesterday
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public string fill { get; set; }
    }

    public class Today
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public string fill { get; set; }
    }

    public class Datasets2
    {
        public List<Yesterday> yesterday { get; set; }
        public List<Today> today { get; set; }
    }

    public class Color5
    {
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
        public string pointBackgroundColor { get; set; }
        public string pointHoverBackgroundColor { get; set; }
        public string pointBorderColor { get; set; }
        public string pointHoverBorderColor { get; set; }
    }

    public class Legend5
    {
        public bool display { get; set; }
    }

    public class Tooltips
    {
        public string position { get; set; }
        public string mode { get; set; }
        public bool intersect { get; set; }
    }

    public class Padding5
    {
        public int left { get; set; }
        public int right { get; set; }
    }

    public class Layout5
    {
        public Padding5 padding { get; set; }
    }

    public class Point3
    {
        public int radius { get; set; }
        public int borderWidth { get; set; }
        public int hoverRadius { get; set; }
        public int hoverBorderWidth { get; set; }
    }

    public class Elements3
    {
        public Point3 point { get; set; }
    }

    public class GridLines2
    {
        public bool display { get; set; }
    }

    public class Ticks6
    {
        public string fontColor { get; set; }
    }

    public class XAx5
    {
        public GridLines2 gridLines { get; set; }
        public Ticks6 ticks { get; set; }
    }

    public class GridLines3
    {
        public int tickMarkLength { get; set; }
    }

    public class Ticks7
    {
        public int stepSize { get; set; }
    }

    public class YAx5
    {
        public GridLines3 gridLines { get; set; }
        public Ticks7 ticks { get; set; }
    }

    public class Scales5
    {
        public List<XAx5> xAxes { get; set; }
        public List<YAx5> yAxes { get; set; }
    }

    public class Filler2
    {
        public bool propagate { get; set; }
    }

    public class Plugins2
    {
        public Filler2 filler { get; set; }
    }

    public class Options5
    {
        public bool spanGaps { get; set; }
        public Legend5 legend { get; set; }
        public bool maintainAspectRatio { get; set; }
        public Tooltips tooltips { get; set; }
        public Layout5 layout { get; set; }
        public Elements3 elements { get; set; }
        public Scales5 scales { get; set; }
        public Plugins2 plugins { get; set; }
    }

    public class Widget5
    {
        public string chartType { get; set; }
        public Datasets2 datasets { get; set; }
        public List<string> labels { get; set; }
        public List<Color5> colors { get; set; }
        public Options5 options { get; set; }
    }

    public class Marker
    {
        public int lat { get; set; }
        public int lng { get; set; }
        public string label { get; set; }
    }

    public class Styler
    {
        public string color { get; set; }
        public string visibility { get; set; }
        public int? saturation { get; set; }
        public int? lightness { get; set; }
    }

    public class Style
    {
        public string featureType { get; set; }
        public string elementType { get; set; }
        public List<Styler> stylers { get; set; }
    }

    public class Widget6
    {
        public List<Marker> markers { get; set; }
        public List<Style> styles { get; set; }
    }

    public class Scheme
    {
        public List<string> domain { get; set; }
    }

    public class Device
    {
        public string name { get; set; }
        public double value { get; set; }
        public double change { get; set; }
        public double nivel { get; set; }
    }

    public class Widget7
    {
        public Scheme scheme { get; set; }
        public List<Device> devices { get; set; }
    }

    public class Scheme2
    {
        public List<string> domain { get; set; }
    }

    public class Change
    {
        public int value { get; set; }
        public double percentage { get; set; }
    }

    public class Series
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Datum
    {
        public string name { get; set; }
        public List<Series> series { get; set; }
    }

    public class Widget8
    {
        public Scheme2 scheme { get; set; }
        public string today { get; set; }
        public Change change { get; set; }
        public List<Datum> data { get; set; }
        public int dataMin { get; set; }
        public int dataMax { get; set; }
    }

    public class Row
    {
        public string title { get; set; }
        public int clicks { get; set; }
        public int conversion { get; set; }
    }

    public class Widget9
    {
        public List<Row> rows { get; set; }
    }

    public class Widgets
    {
        public Widget1 widget1 { get; set; }
        public Widget2 widget2 { get; set; }
        public Widget3 widget3 { get; set; }
        public Widget4 widget4 { get; set; }
        public Widget5 widget5 { get; set; }
        public Widget6 widget6 { get; set; }
        public Widget7 widget7 { get; set; }
        public Widget8 widget8 { get; set; }
        public Widget9 widget9 { get; set; }
    }

    public class RootObject
    {
        public Widgets widgets { get; set; }
    }
}
