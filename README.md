# MvcControls
## ListView
### Prequisites
* Make sure jQuery is loaded first. When using the default sample ASP.NET application, move the line `@Scripts.Render("~/bundles/jquery")` in `_Layout.cshtml` from the bottom to inside the `<head>`.
* Required files:
 * ListView.css
 * listView.js
 * Controls.cs
 * ISelectable.cs

### Usage
* Create a new model class, that implements the `ISelectable` Interface.
* In your view, add this code to the bottom of the file: 
```
@Styles.Render("~/Content/ListView.css")

@section scripts
{
    @Scripts.Render("~/Scripts/listView.js")
}
```
* Use: `@Html.ListViewFor(m => m.MyItems)`
