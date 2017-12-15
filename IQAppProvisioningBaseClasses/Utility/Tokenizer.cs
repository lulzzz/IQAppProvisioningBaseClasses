﻿using Microsoft.SharePoint.Client;

namespace IQAppProvisioningBaseClasses.Utility
{
    public static class Tokenizer
    {
        public static string TokenizeUrls(Web web, string text)
        {
            text = text ?? string.Empty;
            var ctx = web.Context.GetSiteCollectionContext();
            var rootWeb = ctx.Site.RootWeb;
            rootWeb.EnsureProperty(w => w.Url);
            return TokenizeUrls(web, rootWeb, text);
        }

        public static string ReplaceUrlTokens(Web web, string text)
        {
            text = text ?? string.Empty;
            var ctx = web.Context.GetSiteCollectionContext();
            var rootWeb = ctx.Site.RootWeb;
            rootWeb.EnsureProperty(w => w.Url);
            return ReplaceUrlTokens(web, rootWeb, text);
        }

        public static string TokenizeUrls(Web web, Web rootWeb, string text)
        {
            text = text.Replace(web.Url, "{@WebUrl}");
            if (web.ServerRelativeUrl != "/")
            {
                text = text.Replace(web.ServerRelativeUrl, "{@WebServerRelativeUrl}");
                if (web.Url != rootWeb.Url)
                {
                    text = text.Replace(rootWeb.Url, "{@SiteUrl}");
                }
            }
            return text;
        }

        public static string ReplaceUrlTokens(Web web, Web rootWeb, string text)
        {
            text = text ?? string.Empty;
            return text.Replace("{@WebUrl}", web.Url).Replace("{@WebServerRelativeUrl}", web.ServerRelativeUrl).Replace("{@SiteUrl}", rootWeb.Url);
        }
    }
}
