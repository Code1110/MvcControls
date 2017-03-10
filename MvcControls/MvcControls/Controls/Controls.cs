using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using System.Web.WebPages;

namespace MvcControls.Controls
{
    public static class Controls
    {
        public static IHtmlString ListView<TModel>(this HtmlHelper<TModel> helper, IEnumerable<ISelectable> items)
        {
            var ul = new TagBuilder("ul");

            foreach (ISelectable item in items)
            {
                var li = new TagBuilder("li");
                li.MergeAttribute("data-id", item.Id.ToString());
                li.InnerHtml = item.Caption;
                ul.InnerHtml += li.ToString();
            }

            return new HtmlString(ul.ToString());
        }

        public static IHtmlString ListViewFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, IEnumerable<ISelectable>>> items,
            bool showSearchField = true, bool showCheckAll = false)
        {
            // todo: add these optional parameters
            // * DisplayMode: SingleList / DoubleList => http://jqueryui.com/sortable/#connect-lists
            // * AllowSelect per Item
            // * ShowCheckboxes

            // todo: control shoud add js/css automatically

            var metaItems = ModelMetadata.FromLambdaExpression(items, helper.ViewData);
            IEnumerable<ISelectable> listItems = (IEnumerable<ISelectable>)metaItems.Model;
            List<ISelectable> selectables = listItems.ToList();

            string controlId = ExpressionHelper.GetExpressionText(items);

            TagBuilder controlContainer = new TagBuilder("div");
            controlContainer.AddCssClass("listviewcontrolcontainer");
            controlContainer.Attributes.Add("id", controlId);

            TagBuilder controlBody = new TagBuilder("div");
            controlBody.AddCssClass("panel panel-default");

            // Show DisplayName, if specified:
            if (!string.IsNullOrEmpty(metaItems.DisplayName))
            {
                TagBuilder controlLabel = new TagBuilder("div");
                controlLabel.InnerHtml += metaItems.DisplayName;
                controlContainer.InnerHtml += controlLabel.ToString();
            }

            if (showSearchField)
            {
                // <input type="text" id="sc" autocomplete="off" autofocus placeholder="Suchen..." style="width: 230px;" class="form-control" />
                TagBuilder searchField = new TagBuilder("input");
                searchField.Attributes.Add("type", "text");
                searchField.Attributes.Add("id", controlId + "_SearchField");
                searchField.Attributes.Add("autocomplete", "off");
                searchField.Attributes.Add("placeholder", "Suchen...");
                searchField.AddCssClass("form-control");
                // autofocus
                controlBody.InnerHtml += searchField.ToString();
            }

            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("listviewul");

            // Add all the items:
            for (int i = 0; i < selectables.Count; i++)
            {
                TagBuilder li = new TagBuilder("li");
                li.Attributes.Add("data-id", selectables[i].Id.ToString());
                li.InnerHtml = selectables[i].Caption;
                li.AddCssClass("well");

                if (selectables[i].IsSelected)
                {
                    li.Attributes.Add("data-checked", "true");
                    li.AddCssClass("selectedlistviewitem");
                }

                // Alles was nun hier nicht gebunden wird, geht verloren, auch wenn es im ctor des Models eigentlich geladen wird:
                MvcHtmlString textboxSelected = helper.Hidden($"{controlId}[{i}].{nameof(ISelectable.IsSelected)}", selectables[i].IsSelected);
                MvcHtmlString textboxCaption = helper.Hidden($"{controlId}[{i}].{nameof(ISelectable.Caption)}", selectables[i].Caption);
                MvcHtmlString textboxId = helper.Hidden($"{controlId}[{i}].{nameof(ISelectable.Id)}", selectables[i].Id);

                ul.InnerHtml += li.ToString() + textboxSelected + textboxCaption + textboxId;
            }

            TagBuilder itemsPanel = new TagBuilder("div");
            itemsPanel.AddCssClass("listviewitemspanel");
            itemsPanel.InnerHtml += ul;

            // Lambdas und so:
            // http://stackoverflow.com/questions/3813340/get-value-from-asp-net-mvc-lambda-expression
            // http://stackoverflow.com/questions/21172443/how-do-the-mvc-html-helpers-use-expressions-to-get-an-objects-property
            // https://msdn.microsoft.com/en-us/library/bb397687.aspx
            // http://stackoverflow.com/questions/2789504/get-the-property-as-a-string-from-an-expressionfunctmodel-tproperty?noredirect=1&lq=1
            // http://stackoverflow.com/questions/793571/why-would-you-use-expressionfunct-rather-than-funct

            // Dann das:
            //http://odetocode.com/blogs/scott/archive/2012/11/26/why-all-the-lambdas.aspx
            //http://codereview.stackexchange.com/questions/52125/custom-htmlhelper-to-render-a-grid
            //http://gunnarpeipman.com/2013/11/using-model-property-of-type-ienumerablet-for-table-column-headers-in-asp-net-mvc/

            // Checkbox list:
            // http://stackoverflow.com/questions/16688170/asp-net-mvc-html-checkboxfor
            // https://www.codeproject.com/articles/292050/checkboxlist-for-a-missing-mvc-extension


            StringBuilder js = new StringBuilder();
            js.AppendLine("<script type=\"text/javascript\">");
            js.AppendLine("$(document).ready(function() {");
            js.AppendLine($"initListView('{controlId}');");
            js.AppendLine("});");
            js.AppendLine("</script>");

            controlBody.InnerHtml += itemsPanel;
            controlContainer.InnerHtml += controlBody.ToString();

            if (showCheckAll)
            {
                TagBuilder checkAll = new TagBuilder("a");
                checkAll.Attributes.Add("href", $"javascript:checkAllItems('{controlId}');");
                checkAll.InnerHtml += "Select All";
                controlContainer.InnerHtml += checkAll;

                controlContainer.InnerHtml += "&nbsp;|&nbsp;";

                TagBuilder uncheckAll = new TagBuilder("a");
                uncheckAll.Attributes.Add("href", $"javascript:uncheckAllItems('{controlId}');");
                uncheckAll.InnerHtml += "Deselect All";
                controlContainer.InnerHtml += uncheckAll;
            }

            return new HtmlString(controlContainer + js.ToString());
        }

        public static IHtmlString ListView2For<TModel>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, IEnumerable<ISelectable>>> items,
            Expression<Func<TModel, IEnumerable<string>>> selectedItems)
        {

            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes.Add("id", ExpressionHelper.GetExpressionText(items));
            ul.AddCssClass("listviewul");

            var metaItems = ModelMetadata.FromLambdaExpression(items, helper.ViewData);
            IEnumerable<ISelectable> listItems = (IEnumerable<ISelectable>)metaItems.Model;
            List<ISelectable> selectables = listItems.ToList();

            var metaSelected = ModelMetadata.FromLambdaExpression(selectedItems, helper.ViewData);
            List<string> selectedItemsL = (List<string>)metaSelected.Model;

            if (selectedItemsL == null) selectedItemsL = new List<string>();

            int i = 0;

            foreach (ISelectable item in selectables)
            {
                // <li>
                TagBuilder li = new TagBuilder("li");
                li.Attributes.Add("data-id", item.Id.ToString());
                li.InnerHtml = item.Caption;

                // checkbox
                //Dictionary<string, object> attrs = new Dictionary<string, object>();
                //attrs.Add("value", item.Id.ToString());
                //IHtmlString checkbox = helper.CheckBox(ExpressionHelper.GetExpressionText(selectedItems), htmlAttributes: attrs);

                // hidden
                Dictionary<string, object> attrs = new Dictionary<string, object>();
                attrs.Add("data-id", item.Id.ToString());

                string selid = "";

                if (selectedItemsL.Contains(item.Id.ToString()))
                {
                    selid = item.Id.ToString();
                    li.Attributes.Add("data-checked", "true");
                    li.AddCssClass("sel");
                }

                IHtmlString hidden = helper.Hidden($"{ExpressionHelper.GetExpressionText(selectedItems)}[{i}]", selid, attrs);

                //ul.InnerHtml += li + checkbox.ToHtmlString();
                ul.InnerHtml += li.ToString() + hidden.ToHtmlString();
                i++;
            }

            return new HtmlString(ul.ToString());
        }
    }
}