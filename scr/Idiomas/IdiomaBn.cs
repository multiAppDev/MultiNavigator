// Resources/IdiomaBn.cs
namespace Multinavigator.Idiomas
{
    internal class IdiomaBn : IIdiomaStrings
    {
        public string About_Github            => "GitHub-এ কোড দেখুন";
        public string About_Github_button             => "⭐ GitHub-এ স্টার দিন";
        public string Theme_ColorDarkWebContent      => "ডার্ক মোডে वेब কন্টেন্ট";
        public static readonly IdiomaBn Instance = new();
        // =============================================
        // CONFIGURACION
        // =============================================
        public string Cfg_WindowTitle                    => "সেটিংস";
        public string Cfg_Title                          => "সেটিংস";
        public string Cfg_Language_Label                 => "ভাষা";
        public string Cfg_Language_Title                 => "ভাষা";
        public string Cfg_Language_RestartHint           => "প্রতিটি উইন্ডো পুনরায় খুললে ভাষা প্রয়োগ হবে।";

        public string Cfg_TabGeneral                     => "সাধারণ";
        public string Cfg_TabApariencia                  => "চেহারা";
        public string Cfg_TabPrivacidad                  => "গোপনীয়তা";
        public string Cfg_TabPermisos                    => "অনুমতি";
        public string Cfg_TabAvanzado                    => "উন্নত";

        public string Cfg_HomePages_Title                => "হোম পেজ";
        public string Cfg_HomePages_Desc                 => "প্রতিটি প্যানেল শুরুতে খোলার URL";
        public string Cfg_HomePages_Panel1               => "প্যানেল ১";
        public string Cfg_HomePages_Panel2               => "প্যানেল ২";
        public string Cfg_HomePages_Panel3               => "প্যানেল ৩";
        public string Cfg_HomePages_Panel4               => "প্যানেল ৪";
        public string Cfg_HomePages_Save                 => "হোম পেজ সংরক্ষণ করুন";

        public string Cfg_Session_Title                  => "সেশন";
        public string Cfg_Session_Restore                => "স্টার্টআপে আগের সেশন পুনরুদ্ধার করুন";

        public string Cfg_Search_Title                   => "ডিফল্ট সার্চ ইঞ্জিন";
        public string Cfg_Search_Desc                    => "ঠিকানা বারে টাইপ করার সময় ব্যবহৃত হয়";
        public string Cfg_Search_Save                    => "সংরক্ষণ করুন";
        public string Cfg_Search_CustomUrl               => "কাস্টম URL:";

        public string Cfg_Backup_Title                   => "ব্যাকআপ";
        public string Cfg_Backup_Desc                    => "আপনার সেটিংস রপ্তানি বা আমদানি করুন";
        public string Cfg_Backup_Favorites               => "প্রিয়";
        public string Cfg_Backup_Themes                  => "থিম";
        public string Cfg_Backup_Permissions             => "অনুমতি";
        public string Cfg_Backup_Settings                => "সেটিংস";
        public string Cfg_Backup_History                 => "ইতিহাস";
        public string Cfg_Backup_Export                  => "সেটিংস রপ্তানি করুন";
        public string Cfg_Backup_Import                  => "সেটিংস আমদানি করুন";
        public string Cfg_Backup_NoCookies               => "কুকি ব্যাকআপে অন্তর্ভুক্ত নয়";

        public string Cfg_Appearance_Themes_Title        => "থিম";
        public string Cfg_Appearance_Colors_Title        => "কাস্টম রং";
        public string Cfg_Appearance_OpenEditor          => "থিম সম্পাদক খুলুন";
        public string Cfg_Appearance_EditorDesc          => "কাস্টম থিম তৈরি ও সম্পাদনা করুন";
        public string Cfg_Appearance_Preview             => "প্রিভিউ";
        public string Cfg_Appearance_Tab1                => "ট্যাব";
        public string Cfg_Appearance_Tab2                => "সক্রিয় ট্যাব";
        public string Cfg_Appearance_Incognito           => "ইনকগনিটো";
        public string Cfg_Appearance_Options             => "বিকল্প";
        public string Cfg_Appearance_DarkIncognito       => "ইনকগনিটো ট্যাবে স্বয়ংক্রিয় ডার্ক মোড";

        public string Cfg_Privacy_Title                  => "গোপনীয়তা ও ডেটা";
        public string Cfg_Privacy_Cookies                => "কুকি";
        public string Cfg_Privacy_CookiesUnit            => "ডিস্কে KB";
        public string Cfg_Privacy_CookiesManage          => "পরিচালনা";
        public string Cfg_Privacy_DeleteAll              => "সব মুছুন";
        public string Cfg_Privacy_History                => "ইতিহাস";
        public string Cfg_Privacy_HistoryUnit            => "এন্ট্রি";
        public string Cfg_Privacy_HistoryView            => "ইতিহাস দেখুন";
        public string Cfg_Privacy_Cache                  => "ক্যাশ";
        public string Cfg_Privacy_CacheUnit              => "ডিস্কে MB";
        public string Cfg_Privacy_CacheDelete            => "ক্যাশ মুছুন";

        public string Cfg_Perms_GlobalTitle              => "বৈশ্বিক অনুমতি";
        public string Cfg_Perms_DomainTitle              => "ডোমেইন অনুযায়ী অনুমতি";
        public string Cfg_Perms_Camera                   => "ক্যামেরা";
        public string Cfg_Perms_Mic                      => "মাইক্রোফোন";
        public string Cfg_Perms_Location                 => "অবস্থান";
        public string Cfg_Perms_Notifications            => "বিজ্ঞপ্তি";
        public string Cfg_Perms_DomainCol                => "ডোমেইন";
        public string Cfg_Perms_AddDomain                => "ডোমেইন যোগ করুন";
        public string Cfg_Perms_Reset                    => "অনুমতি রিসেট করুন";

        public string Cfg_Adv_Title                      => "উন্নত WebView2 বিকল্প";
        public string Cfg_Adv_Warning                    => "⚠ প্রয়োগ করতে অ্যাপ্লিকেশন পুনরায় চালু করতে হবে";
        public string Cfg_Adv_BgNetworking               => "ব্যাকগ্রাউন্ড নেটওয়ার্কিং অক্ষম করুন";
        public string Cfg_Adv_BgNetworking_Desc          => "ব্রাউজার ব্যাকগ্রাউন্ডে থাকলে নেটওয়ার্ক সংযোগ বন্ধ রাখুন";
        public string Cfg_Adv_Sync                       => "সিঙ্ক্রোনাইজেশন অক্ষম করুন";
        public string Cfg_Adv_Sync_Desc                  => "Google সার্ভারের সাথে ডেটা সিঙ্ক বন্ধ করুন";
        public string Cfg_Adv_Translate                  => "অনুবাদক অক্ষম করুন";
        public string Cfg_Adv_Translate_Desc             => "স্বয়ংক্রিয় পেজ অনুবাদ সেবা সরান";
        public string Cfg_Adv_Extensions                 => "এক্সটেনশন অক্ষম করুন";
        public string Cfg_Adv_Extensions_Desc            => "ব্রাউজার এক্সটেনশন লোড বন্ধ করুন";
        public string Cfg_Adv_DefaultApps                => "ডিফল্ট অ্যাপ অক্ষম করুন";
        public string Cfg_Adv_DefaultApps_Desc           => "Chrome ডিফল্ট অ্যাপ ইনস্টল বন্ধ করুন";
        public string Cfg_Adv_DefaultBrowserCheck        => "ডিফল্ট ব্রাউজার চেক এড়িয়ে যান";
        public string Cfg_Adv_DefaultBrowserCheck_Desc   => "ডিফল্ট ব্রাউজার কিনা তা যাচাই বা অনুরোধ করে না";
        public string Cfg_Adv_Metrics                    => "শুধুমাত্র মেট্রিক লগিং";
        public string Cfg_Adv_Metrics_Desc               => "মেট্রিক স্থানীয়ভাবে লগ হয় কিন্তু পাঠানো হয় না";
        public string Cfg_Adv_Breakpad                   => "ত্রুটি রিপোর্ট অক্ষম করুন";
        public string Cfg_Adv_Breakpad_Desc              => "স্বয়ংক্রিয় ক্র্যাশ রিপোর্ট পাঠানো বন্ধ করুন";
        public string Cfg_Adv_Phishing                   => "ফিশিং সনাক্তকরণ অক্ষম করুন";
        public string Cfg_Adv_Phishing_Desc              => "ক্লায়েন্ট-সাইড ফিশিং সাইট সনাক্তকরণ বন্ধ করুন";
        public string Cfg_Adv_HangMonitor                => "হ্যাং মনিটর অক্ষম করুন";
        public string Cfg_Adv_HangMonitor_Desc           => "ব্রাউজার হ্যাং সনাক্ত বা রিপোর্ট করে না";
        public string Cfg_Adv_Repost                     => "ফর্ম রিসাবমিট সতর্কতা এড়িয়ে যান";
        public string Cfg_Adv_Repost_Desc                => "POST রিফ্রেশে নিশ্চিতকরণ ডায়ালগ দেখাবে না";
        public string Cfg_Adv_DomainReliability          => "ডোমেইন টেলিমেট্রি অক্ষম করুন";
        public string Cfg_Adv_DomainReliability_Desc     => "Google-এ ডোমেইন নির্ভরযোগ্যতা ডেটা পাঠানো বন্ধ করুন";
        public string Cfg_Adv_ComponentUpdate            => "কম্পোনেন্ট আপডেট অক্ষম করুন";
        public string Cfg_Adv_ComponentUpdate_Desc       => "স্বয়ংক্রিয় কম্পোনেন্ট আপডেট ডাউনলোড বন্ধ করুন";
        public string Cfg_Adv_BgTimer                    => "টাইমার থ্রটলিং অক্ষম করুন";
        public string Cfg_Adv_BgTimer_Desc               => "ব্যাকগ্রাউন্ড ট্যাবে JS টাইমার ধীর হবে না";
        public string Cfg_Adv_RendererBg                 => "ব্যাকগ্রাউন্ড কম অগ্রাধিকার অক্ষম করুন";
        public string Cfg_Adv_RendererBg_Desc            => "ট্যাব লুকানো থাকলেও রেন্ডারার অগ্রাধিকার বজায় রাখুন";
        public string Cfg_Adv_IpcFlood                   => "IPC ফ্লাড সুরক্ষা অক্ষম করুন";
        public string Cfg_Adv_IpcFlood_Desc              => "ব্রাউজার প্রক্রিয়াগুলোর মধ্যে IPC বার্তার সীমা সরান";
        public string Cfg_Adv_StateOn                    => "✔ সক্রিয়";
        public string Cfg_Adv_StateOff                   => "✖ নিষ্ক্রিয়";

        public string Cfg_Msg_ConfirmDeleteTheme         => "'{0}' থিমটি মুছবেন?";
        public string Cfg_Msg_ConfirmDeleteThemeTitle    => "মোছার নিশ্চিতকরণ";
        public string Cfg_Msg_NoTabInitialized           => "এখনও কোনো ট্যাব শুরু হয়নি।";
        public string Cfg_Msg_ConfirmDeleteHistory       => "সম্পূর্ণ ইতিহাস মুছবেন?";
        public string Cfg_Msg_ConfirmDeleteCookies       => "সব কুকি মুছবেন?";
        public string Cfg_Msg_Confirmation               => "নিশ্চিতকরণ";
        public string Cfg_Msg_CookiesDeleted             => "কুকি মুছে ফেলা হয়েছে।";
        public string Cfg_Msg_NoActiveTabCache           => "ক্যাশ মুছতে কোনো সক্রিয় ট্যাব নেই।";
        public string Cfg_Msg_CacheDeleted               => "ক্যাশ মুছে ফেলা হয়েছে।";
        public string Cfg_Msg_HomePagesSaved             => "হোম পেজ সংরক্ষিত হয়েছে।";
        public string Cfg_Msg_SearchSaved                => "সার্চ ইঞ্জিন সংরক্ষিত হয়েছে।";
        public string Cfg_Msg_ExportOk                   => "সেটিংস সফলভাবে রপ্তানি হয়েছে।";
        public string Cfg_Msg_ImportOk                   => "সেটিংস সফলভাবে আমদানি হয়েছে।\nসব পরিবর্তন প্রয়োগ করতে অ্যাপ পুনরায় চালু করুন।";
        public string Cfg_Msg_ExportError                => "রপ্তানি ত্রুটি: {0}";
        public string Cfg_Msg_ImportError                => "আমদানি ত্রুটি: {0}";
        public string Cfg_Msg_Export                     => "রপ্তানি";
        public string Cfg_Msg_Import                     => "আমদানি";
        public string Cfg_Msg_Saved                      => "সংরক্ষিত";
        public string Cfg_Msg_Done                       => "সম্পন্ন";
        public string Cfg_Msg_Error                      => "ত্রুটি";
        public string Cfg_Msg_Notice                     => "বিজ্ঞপ্তি";

        // =============================================
        // COOKIEMANAGERWINDOW
        // =============================================
        public string Cookie_WindowTitle     => "কুকি ম্যানেজার";
        public string Cookie_SearchDomain    => "ডোমেইন খুঁজুন:";
        public string Cookie_Reload          => "পুনরায় লোড করুন";
        public string Cookie_Domains         => "ডোমেইন";
        public string Cookie_Cookies         => "কুকি";
        public string Cookie_SearchCookie    => "কুকি খুঁজুন:";
        public string Cookie_ColName         => "নাম";
        public string Cookie_ColValue        => "মান";
        public string Cookie_ColPath         => "পথ";
        public string Cookie_ColExpires      => "মেয়াদ শেষ";
        public string Cookie_Details         => "বিবরণ";
        public string Cookie_DetailName      => "নাম:";
        public string Cookie_DetailValue     => "মান:";
        public string Cookie_DetailDomain    => "ডোমেইন:";
        public string Cookie_DetailPath      => "পথ:";
        public string Cookie_DetailExpires   => "মেয়াদ শেষ:";
        public string Cookie_DetailFlags     => "ফ্ল্যাগ:";
        public string Cookie_FlagsFormat     => "Secure={0}, HttpOnly={1}, Session={2}";
        public string Cookie_CopyValue       => "মান কপি করুন";
        public string Cookie_EditCookie      => "কুকি সম্পাদনা করুন";
        public string Cookie_DeleteCookie    => "কুকি মুছুন";
        public string Cookie_DeleteDomain    => "ডোমেইন কুকি মুছুন";
        public string Cookie_DeleteAll       => "সব কুকি মুছুন";
        public string Cookie_Close           => "বন্ধ করুন";

        // =============================================
        // EDITCOOKIEWINDOW
        // =============================================
        public string EditCookie_WindowTitle     => "কুকি সম্পাদনা";
        public string EditCookie_Name            => "নাম:";
        public string EditCookie_Value           => "মান:";
        public string EditCookie_Path            => "পথ:";
        public string EditCookie_Expires         => "মেয়াদ শেষের তারিখ:";
        public string EditCookie_Save            => "সংরক্ষণ করুন";
        public string EditCookie_Cancel          => "বাতিল করুন";
        public string EditCookie_Msg_EmptyValue  => "কুকির মান খালি রাখা যাবে না।";
        public string EditCookie_Msg_EmptyPath   => "পথ খালি রাখা যাবে না।";
        public string EditCookie_Msg_PathSlash   => "পথ অবশ্যই '/' দিয়ে শুরু হতে হবে।";
        public string EditCookie_Msg_NoDate      => "একটি মেয়াদ শেষের তারিখ বেছে নিতে হবে।";
        public string EditCookie_Msg_PastDate    => "মেয়াদ শেষের তারিখ অতীতে হতে পারবে না।";
        public string EditCookie_Msg_SaveError   => "কুকি সংরক্ষণ ত্রুটি: {0}";

        // =============================================
        // FAVORITEEDITDIALOG
        // =============================================
        public string FavDlg_WindowTitle       => "বুকমার্ক";
        public string FavDlg_Title             => "শিরোনাম:";
        public string FavDlg_Url               => "URL:";
        public string FavDlg_Folder            => "গ্রুপ/ফোল্ডার:";
        public string FavDlg_Favicon           => "ফেভিকন URL:";
        public string FavDlg_FaviconTooltip    => "ঐচ্ছিক — স্বয়ংক্রিয় সনাক্তকরণের জন্য খালি রাখুন";
        public string FavDlg_Save              => "সংরক্ষণ করুন";
        public string FavDlg_Cancel            => "বাতিল করুন";
        public string FavDlg_Msg_UrlRequired   => "URL আবশ্যক।";
        public string FavDlg_Msg_Validation    => "যাচাইকরণ";

        // =============================================
        // FAVORITESWINDOW
        // =============================================
        public string Fav_WindowTitle        => "বুকমার্ক ম্যানেজার";
        public string Fav_Add                => "➕ যোগ করুন";
        public string Fav_Edit               => "✏ সম্পাদনা";
        public string Fav_Delete             => "🗑 মুছুন";
        public string Fav_Up                 => "▲ উপরে";
        public string Fav_Down               => "▼ নিচে";
        public string Fav_Open               => "🌐 খুলুন";
        public string Fav_OpenTooltip        => "ব্রাউজারে খুলুন (বা সারিতে ডবল-ক্লিক করুন)";
        public string Fav_CopyUrl            => "📋 URL কপি করুন";
        public string Fav_Close              => "✖ বন্ধ করুন";
        public string Fav_Export             => "রপ্তানি:";
        public string Fav_ExportHtml         => "📄 HTML";
        public string Fav_ExportHtmlTooltip  => "Netscape Bookmark HTML — Chrome, Edge, Firefox-এ আমদানি করা যাবে";
        public string Fav_ExportJson         => "💾 JSON";
        public string Fav_ExportCsv          => "📊 CSV";
        public string Fav_Import             => "আমদানি:";
        public string Fav_ImportHtml         => "📂 HTML";
        public string Fav_ImportHtmlTooltip  => "Netscape Bookmark HTML (যেকোনো ব্রাউজার থেকে রপ্তানি করা)";
        public string Fav_ImportJson         => "📂 JSON";
        public string Fav_ImportFrom         => "বুকমার্ক আমদানি করুন:";
        public string Fav_ImportBtn          => "আমদানি";
        public string Fav_Search             => "🔍 খুঁজুন:";
        public string Fav_Group              => "  গ্রুপ:";
        public string Fav_Clear              => "ফিল্টার সাফ করুন";
        public string Fav_AllGroups          => "(সব)";
        public string Fav_ColTitle           => "শিরোনাম";
        public string Fav_ColGroup           => "গ্রুপ";
        public string Fav_CtxOpen            => "🌐 ব্রাউজারে খুলুন";
        public string Fav_CtxEdit            => "✏ সম্পাদনা";
        public string Fav_CtxCopy            => "📋 URL কপি করুন";
        public string Fav_CtxDelete          => "🗑 মুছুন";
        public string Fav_Msg_Duplicate      => "এই URL-এর একটি বুকমার্ক ইতিমধ্যে আছে।";
        public string Fav_Msg_DuplicateTitle => "ডুপ্লিকেট";
        public string Fav_Msg_ConfirmDelete  => "{0}টি বুকমার্ক মুছবেন?";
        public string Fav_Msg_Confirm        => "নিশ্চিত করুন";
        public string Fav_Msg_ImportResult   => "আমদানি সম্পন্ন:\n\n  ✔ যোগ করা হয়েছে: {0}\n  ⏭ এড়িয়ে গেছে: {1} (ডুপ্লিকেট)";
        public string Fav_Msg_ImportTitle    => "আমদানি";
        public string Fav_Msg_ImportJsonError => "ত্রুটি: {0}";
        public string Fav_Msg_ImportJsonTitle => "JSON আমদানি";
        public string Fav_Msg_BrowserNotFound => "{0}-এর জন্য কোনো বুকমার্ক পাওয়া যায়নি।\n{1}";
        public string Fav_Msg_NotFound       => "পাওয়া যায়নি";
        public string Fav_Msg_FirefoxNotFound => "Firefox প্রোফাইল ফোল্ডার পাওয়া যায়নি।";
        public string Fav_Msg_FirefoxFallback => "Firefox-এর places.sqlite পড়া যাচ্ছে না (Firefox চলছে হতে পারে)।\n\nবিকল্প: Firefox-এ যান\n  বুকমার্ক ▸ বুকমার্ক পরিচালনা ▸ আমদানি ও ব্যাকআপ ▸ HTML-এ বুকমার্ক রপ্তানি করুন\nতারপর এখানে 'HTML আমদানি' ব্যবহার করুন।\n\nএখন HTML আমদানি খুলবেন?";
        public string Fav_Msg_FirefoxTitle   => "Firefox";
        public string Fav_Msg_SqliteRequired => "Firefox ডেটাবেস পড়তে Microsoft.Data.Sqlite NuGet প্যাকেজ ইনস্টল করুন।";
        public string Fav_Msg_ErrorOpenUrl   => "URL খোলার ত্রুটি";
        public string Fav_StatusReady        => "প্রস্তুত";
        public string Fav_StatusAdded        => "যোগ করা হয়েছে: {0}";
        public string Fav_StatusEdited       => "সম্পাদিত: {0}";
        public string Fav_StatusDeleted      => "মুছে ফেলা হয়েছে: {0}";
        public string Fav_StatusUrlCopied    => "URL ক্লিপবোর্ডে কপি করা হয়েছে";
        public string Fav_StatusExported     => "{0} রপ্তানি → {1}";
        public string Fav_StatusImported     => "{0}টি বুকমার্ক আমদানি — {1}টি ডুপ্লিকেট এড়িয়ে গেছে";
        public string Fav_Count              => "{0} / {1} বুকমার্ক";

        // =============================================
        // HISTORYWINDOW
        // =============================================
        public string History_WindowTitle        => "ব্রাউজিং ইতিহাস";
        public string History_Title              => "🕐 ইতিহাস";
        public string History_IncognitoNote      => "ইনকগনিটো মোডে পরিদর্শন করা সাইটগুলো এখানে দেখা যায় না।";
        public string History_DeleteAll          => "🗑 সব মুছুন";
        public string History_Today              => "আজ";
        public string History_Yesterday          => "গতকাল";
        public string History_ThisWeek           => "এই সপ্তাহ";
        public string History_ThisMonth          => "এই মাস";
        public string History_SearchResults      => "\"{0}\" এর ফলাফল";
        public string History_Msg_ConfirmDelete  => "সম্পূর্ণ ইতিহাস মুছবেন?";
        public string History_Msg_Confirmation   => "নিশ্চিতকরণ";

        // =============================================
        // MAINWINDOW
        // =============================================
        public string Main_MaxPanel1            => "প্যানেল ১ বড় করুন";
        public string Main_MaxPanel2            => "প্যানেল ২ বড় করুন";
        public string Main_MaxPanel3            => "প্যানেল ৩ বড় করুন";
        public string Main_MaxPanel4            => "প্যানেল ৪ বড় করুন";
        public string Main_RestorePanels        => "সব প্যানেল পুনরুদ্ধার করুন";
        public string Main_Notes                => "নোট";
        public string Main_WelcomePopup         => "⚙ হোম পেজ ও আরো সেটিংস করুন...";
        public string Main_FavAdd               => "প্রিয়তে যোগ করুন";
        public string Main_FavManage            => "📁 প্রিয় পরিচালনা";
        public string Main_MenuGeneral          => "সাধারণ";
        public string Main_MenuApariencia       => "চেহারা";
        public string Main_MenuFavoritos        => "প্রিয়";
        public string Main_MenuHistorial        => "ইতিহাস";
        public string Main_MenuPrivacidad       => "গোপনীয়তা";
        public string Main_MenuPermisos         => "অনুমতি";
        public string Main_MenuCookies          => "কুকি";
        public string Main_MenuAvanzado         => "উন্নত";
        public string Main_MenuAcercaDe         => "সম্পর্কে...";
        public string Main_CtxClearMark         => "কোনো চিহ্ন নেই";
        public string Main_CtxMarkColor         => "রং দিয়ে চিহ্নিত করুন";
        public string Main_CtxSortByColor       => "রং অনুযায়ী সাজান";
        public string Main_CtxGoHome            => "হোম পেজে যান";
        public string Main_CtxIncognitoOn       => "ইনকগনিটো মোড চালু করুন";
        public string Main_CtxIncognitoOff      => "ইনকগনিটো মোড বন্ধ করুন";
        public string Main_CtxReload            => "পেজ পুনরায় লোড করুন";
        public string Main_CtxOpenInWindow      => "নতুন উইন্ডোতে খুলুন";
        public string Main_CtxOpenWith          => "এর সাথে খুলুন...";
        public string Main_CtxReopenClosed      => "বন্ধ ট্যাব পুনরায় খুলুন";
        public string Main_CtxNewTab            => "নতুন ট্যাব";
        public string Main_CtxNewIncognito      => "নতুন ইনকগনিটো ট্যাব";
        public string Main_CtxMoveTab           => "ট্যাব সরান";
        public string Main_CtxCopyTab           => "ট্যাব কপি করুন";
        public string Main_CtxDuplicate         => "ট্যাব ডুপ্লিকেট করুন (ডানে)";
        public string Main_CtxClose             => "ট্যাব বন্ধ করুন";
        public string Main_CtxRestoreSession    => "শেষ সেশনের ট্যাব পুনরুদ্ধার করুন";
        public string Main_PosRight             => "ডানে";
        public string Main_PosLeft              => "বামে";
        public string Main_PosEnd               => "শেষে";
        public string Main_PosStart             => "শুরুতে";
        public string Main_CloseThis            => "এই ট্যাব";
        public string Main_CloseAllRight        => "ডানের সব";
        public string Main_CloseAllLeft         => "বামের সব";
        public string Main_CloseOthers          => "এটি ছাড়া সব";
        public string Main_Browser1             => "ব্রাউজার ১";
        public string Main_Browser2             => "ব্রাউজার ২";
        public string Main_Browser3             => "ব্রাউজার ৩";
        public string Main_Browser4             => "ব্রাউজার ৪";
        public string Main_PermAllow            => "অনুমতি দিন";
        public string Main_PermBlock            => "ব্লক করুন";
        public string Main_NotesReminderPrefix  => "আপনার ";
        public string Main_NotesReminderSuffix  => "টি মুলতুবি নোট রিমাইন্ডার আছে:\n\n";
        public string Main_NotesReminderQuestion => "\nনোট দেখতে চান?";
        public string Main_NotesReminderTitle   => "নোট রিমাইন্ডার";
        public string Main_NoSession            => "কোনো সংরক্ষিত সেশন নেই।";
        public string Main_RestoreSessionTitle  => "সেশন পুনরুদ্ধার";
        public string Main_SecureNoInfo         => "নিরাপত্তা তথ্য পাওয়া যাচ্ছে না।";
        public string Main_SecureYes            => "সংযোগ নিরাপদ।\nসাইটটি HTTPS ব্যবহার করছে।";
        public string Main_SecureNo             => "সংযোগ নিরাপদ নয়।\nসাইটটি HTTPS ব্যবহার করছে না।";
        public string Main_ErrExternalLink      => "বাহ্যিক লিংক খোলার ত্রুটি: ";
        public string Main_NoTabInit            => "এখনও কোনো ট্যাব শুরু হয়নি।";
        public string Main_ErrOpenBrowser       => "ব্রাউজার খোলা যাচ্ছে না: ";
        public string Main_AboutTitle           => "Multinavigator সম্পর্কে";
        public string Main_AboutNotAvailable    => "পাওয়া যাচ্ছে না";
        public string Main_AboutVersion         => "সংস্করণ: 7.0.0";
        public string Main_AboutChromium        => "Chromium সংস্করণ: ";
        public string Main_AboutLocalIp         => "🏠  স্থানীয় IP: ";
        public string Main_AboutPublicIp        => "🌍  পাবলিক IP: ";
        public string Main_AboutAppName         => "Multinavigator 7";
        public string Main_PermFormat           => "{0} ব্যবহার করতে চায়: {1}";
        public string Main_NewTab               => "নতুন ট্যাব";
        public string Main_Loading              => "লোড হচ্ছে...";

        // =============================================
        // NOTASWINDOW
        // =============================================
        public string Notas_WindowTitle         => "নোট";
        public string Notas_NewNote             => "+ নতুন নোট";
        public string Notas_Reminder            => "রিমাইন্ডার";
        public string Notas_MsgDelete           => "এই নোটটি মুছবেন?";
        public string Notas_MsgConfirm          => "নিশ্চিত করুন";

        // =============================================
        // THEMEEDITOR
        // =============================================
        public string Theme_WindowTitle         => "কাস্টম থিম এডিটর";
        public string Theme_HeaderTitle         => "🎨 থিম এডিটর";
        public string Theme_PreviewTitle        => "প্রিভিউ";
        public string Theme_PreviewTab1         => "ট্যাব ১";
        public string Theme_PreviewTab2         => "ট্যাব ২";
        public string Theme_PreviewIncognito    => "🕵️ ইনকগনিটো";
        public string Theme_PreviewUrl          => "🔒 https://example.com";
        public string Theme_Hue                 => "🎨 হিউ";
        public string Theme_Saturation          => "💧 স্যাচুরেশন";
        public string Theme_Luminosity          => "💡 উজ্জ্বলতা";
        public string Theme_SecGeneral          => "🎨 সাধারণ রং";
        public string Theme_SecTabsNormal       => "📑 সাধারণ ট্যাব";
        public string Theme_SecNavBar           => "🧭 নেভিগেশন বার";
        public string Theme_SecButtons          => "🔘 বোতাম";
        public string Theme_SecButtonCentro     => "🔘 কেন্দ্র বোতাম";
        public string Theme_SecIncognito        => "🕵️ ইনকগনিটো ট্যাব";
        public string Theme_ColorWindowBg       => "উইন্ডো পটভূমি";
        public string Theme_ColorWindowFg       => "উইন্ডো টেক্সট";
        public string Theme_ColorTabInactive    => "নিষ্ক্রিয় ট্যাব";
        public string Theme_ColorTabActive      => "সক্রিয় ট্যাব";
        public string Theme_ColorHover          => "হোভার";
        public string Theme_ColorTabActiveHover => "সক্রিয় + হোভার";
        public string Theme_ColorTabText        => "ট্যাব টেক্সট";
        public string Theme_ColorNavBarBg       => "বার পটভূমি";
        public string Theme_ColorNavBarFg       => "টেক্সট";
        public string Theme_ColorUrlBg          => "URL পটভূমি";
        public string Theme_ColorUrlFg          => "URL টেক্সট";
        public string Theme_ColorButtonAccent   => "বোতামের রং";
        public string Theme_ColorButtonPressed  => "চাপা";
        public string Theme_ColorButtonCentro   => "কেন্দ্র বোতামের রং";
        public string Theme_ColorIncogInactive  => "নিষ্ক্রিয়";
        public string Theme_ColorIncogActive    => "সক্রিয়";
        public string Theme_ColorIncogText      => "টেক্সট";
        public string Theme_LabelName           => "নাম:";
        public string Theme_BtnSave             => "💾 সংরক্ষণ করুন";
        public string Theme_BtnCancel           => "❌ বাতিল";
        public string Theme_PickerTitle         => "রং বাছাইকারী";
        public string Theme_PickerHue           => "হিউ (H)";
        public string Theme_PickerSaturation    => "স্যাচুরেশন (S)";
        public string Theme_PickerLuminosity    => "উজ্জ্বলতা (L)";
        public string Theme_PickerOk            => "ঠিক আছে";
        public string Theme_PickerCancel        => "বাতিল";
        public string Theme_MsgInitError        => "এডিটর শুরু করতে ত্রুটি: ";
        public string Theme_MsgInitErrorTitle   => "ত্রুটি";
        public string Theme_MsgNoName           => "থিমের জন্য একটি নাম দিন।";
        public string Theme_MsgNoNameTitle      => "নাম প্রয়োজন";
        public string Theme_MsgPredefined       => "একটি পূর্বনির্ধারিত থিম এবং ওভাররাইট করা যাবে না।\nভিন্ন নাম বেছে নিন।";
        public string Theme_MsgPredefinedTitle  => "সংরক্ষিত নাম";
        public string Theme_MsgOverwriteFormat  => "'{0}' নামে একটি কাস্টম থিম ইতিমধ্যে আছে।\nওভাররাইট করবেন?";
        public string Theme_MsgOverwriteTitle   => "বিদ্যমান থিম";
        public string Theme_MsgSavedFormat      => "থিম '{0}' সফলভাবে সংরক্ষিত হয়েছে।";
        public string Theme_MsgSavedTitle       => "থিম সংরক্ষিত";
    }
}
