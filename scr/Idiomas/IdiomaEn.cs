// Resources/IdiomaEn.cs
namespace Multinavigator.Idiomas
{
    internal class IdiomaEn : IIdiomaStrings
    {
        public string About_Github            => "View code on GitHub";
        public string About_Github_button             => "⭐ Star on GitHub";
        public string Theme_ColorDarkWebContent      => "Web content in dark mode";
        public static readonly IdiomaEn Instance = new();
        // =============================================
        // CONFIGURACION
        // =============================================
        public string Cfg_WindowTitle                    => "Settings";
        public string Cfg_Title                          => "Settings";
        public string Cfg_Language_Label                 => "Language";
        public string Cfg_Language_Title                 => "Language";
        public string Cfg_Language_RestartHint           => "The language will apply when each window is reopened.";

        public string Cfg_TabGeneral                     => "General";
        public string Cfg_TabApariencia                  => "Appearance";
        public string Cfg_TabPrivacidad                  => "Privacy";
        public string Cfg_TabPermisos                    => "Permissions";
        public string Cfg_TabAvanzado                    => "Advanced";

        public string Cfg_HomePages_Title                => "Home pages";
        public string Cfg_HomePages_Desc                 => "URL that opens when each panel starts";
        public string Cfg_HomePages_Panel1               => "Panel 1";
        public string Cfg_HomePages_Panel2               => "Panel 2";
        public string Cfg_HomePages_Panel3               => "Panel 3";
        public string Cfg_HomePages_Panel4               => "Panel 4";
        public string Cfg_HomePages_Save                 => "Save home pages";

        public string Cfg_Session_Title                  => "Session";
        public string Cfg_Session_Restore                => "Restore previous session on startup";

        public string Cfg_Search_Title                   => "Default search engine";
        public string Cfg_Search_Desc                    => "Used when typing in the address bar";
        public string Cfg_Search_Save                    => "Save";
        public string Cfg_Search_CustomUrl               => "Custom URL:";

        public string Cfg_Backup_Title                   => "Backup";
        public string Cfg_Backup_Desc                    => "Export or import your settings";
        public string Cfg_Backup_Favorites               => "Favorites";
        public string Cfg_Backup_Themes                  => "Themes";
        public string Cfg_Backup_Permissions             => "Permissions";
        public string Cfg_Backup_Settings                => "Settings";
        public string Cfg_Backup_History                 => "History";
        public string Cfg_Backup_Export                  => "Export settings";
        public string Cfg_Backup_Import                  => "Import settings";
        public string Cfg_Backup_NoCookies               => "Cookies are not included in the backup";

        public string Cfg_Appearance_Themes_Title        => "Themes";
        public string Cfg_Appearance_Colors_Title        => "Custom colors";
        public string Cfg_Appearance_OpenEditor          => "Open theme editor";
        public string Cfg_Appearance_EditorDesc          => "Create and edit custom themes";
        public string Cfg_Appearance_Preview             => "Preview";
        public string Cfg_Appearance_Tab1                => "Tab";
        public string Cfg_Appearance_Tab2                => "Active tab";
        public string Cfg_Appearance_Incognito           => "Incognito";
        public string Cfg_Appearance_Options             => "Options";
        public string Cfg_Appearance_DarkIncognito       => "Automatic dark mode on incognito tabs";

        public string Cfg_Privacy_Title                  => "Privacy and data";
        public string Cfg_Privacy_Cookies                => "Cookies";
        public string Cfg_Privacy_CookiesUnit            => "KB on disk";
        public string Cfg_Privacy_CookiesManage          => "Manage";
        public string Cfg_Privacy_DeleteAll              => "Delete all";
        public string Cfg_Privacy_History                => "History";
        public string Cfg_Privacy_HistoryUnit            => "entries";
        public string Cfg_Privacy_HistoryView            => "View history";
        public string Cfg_Privacy_Cache                  => "Cache";
        public string Cfg_Privacy_CacheUnit              => "MB on disk";
        public string Cfg_Privacy_CacheDelete            => "Clear cache";

        public string Cfg_Perms_GlobalTitle              => "Global permissions";
        public string Cfg_Perms_DomainTitle              => "Per-domain permissions";
        public string Cfg_Perms_Camera                   => "Camera";
        public string Cfg_Perms_Mic                      => "Microphone";
        public string Cfg_Perms_Location                 => "Location";
        public string Cfg_Perms_Notifications            => "Notifications";
        public string Cfg_Perms_DomainCol                => "Domain";
        public string Cfg_Perms_AddDomain                => "Add domain";
        public string Cfg_Perms_Reset                    => "Reset permissions";

        public string Cfg_Adv_Title                      => "Advanced WebView2 options";
        public string Cfg_Adv_Warning                    => "⚠ Requires restarting the application to take effect";
        public string Cfg_Adv_BgNetworking               => "Disable background networking";
        public string Cfg_Adv_BgNetworking_Desc          => "Blocks network connections when the browser is in the background";
        public string Cfg_Adv_Sync                       => "Disable sync";
        public string Cfg_Adv_Sync_Desc                  => "Prevents data synchronisation with Google servers";
        public string Cfg_Adv_Translate                  => "Disable translator";
        public string Cfg_Adv_Translate_Desc             => "Removes the automatic page translation service";
        public string Cfg_Adv_Extensions                 => "Disable extensions";
        public string Cfg_Adv_Extensions_Desc            => "Prevents browser extensions from loading";
        public string Cfg_Adv_DefaultApps                => "Disable default apps";
        public string Cfg_Adv_DefaultApps_Desc           => "Prevents Chrome default applications from being installed";
        public string Cfg_Adv_DefaultBrowserCheck        => "Skip default browser check";
        public string Cfg_Adv_DefaultBrowserCheck_Desc   => "Does not check or request to be the default browser";
        public string Cfg_Adv_Metrics                    => "Metrics recording only";
        public string Cfg_Adv_Metrics_Desc               => "Metrics are recorded locally but not sent";
        public string Cfg_Adv_Breakpad                   => "Disable crash reports";
        public string Cfg_Adv_Breakpad_Desc              => "Disables automatic crash report submission";
        public string Cfg_Adv_Phishing                   => "Disable phishing detection";
        public string Cfg_Adv_Phishing_Desc              => "Disables client-side phishing site detection";
        public string Cfg_Adv_HangMonitor                => "Disable hang monitor";
        public string Cfg_Adv_HangMonitor_Desc           => "Does not detect or report when the browser freezes";
        public string Cfg_Adv_Repost                     => "Skip form resubmission warning";
        public string Cfg_Adv_Repost_Desc                => "Does not show the confirmation dialog when reloading a POST";
        public string Cfg_Adv_DomainReliability          => "Disable domain telemetry";
        public string Cfg_Adv_DomainReliability_Desc     => "Prevents domain reliability data from being sent to Google";
        public string Cfg_Adv_ComponentUpdate            => "Disable component updates";
        public string Cfg_Adv_ComponentUpdate_Desc       => "Prevents automatic component update downloads";
        public string Cfg_Adv_BgTimer                    => "Disable timer throttling";
        public string Cfg_Adv_BgTimer_Desc               => "JS timers are not slowed down in background tabs";
        public string Cfg_Adv_RendererBg                 => "Disable background low priority";
        public string Cfg_Adv_RendererBg_Desc            => "The renderer keeps its priority even when the tab is hidden";
        public string Cfg_Adv_IpcFlood                   => "Disable IPC flood protection";
        public string Cfg_Adv_IpcFlood_Desc              => "Removes the IPC message limit between browser processes";
        public string Cfg_Adv_StateOn                    => "✔ Enabled";
        public string Cfg_Adv_StateOff                   => "✖ Disabled";

        public string Cfg_Msg_ConfirmDeleteTheme         => "Delete theme '{0}'?";
        public string Cfg_Msg_ConfirmDeleteThemeTitle    => "Confirm deletion";
        public string Cfg_Msg_NoTabInitialized           => "No tab has been initialised yet.";
        public string Cfg_Msg_ConfirmDeleteHistory       => "Delete all history?";
        public string Cfg_Msg_ConfirmDeleteCookies       => "Delete all cookies?";
        public string Cfg_Msg_Confirmation               => "Confirmation";
        public string Cfg_Msg_CookiesDeleted             => "Cookies deleted.";
        public string Cfg_Msg_NoActiveTabCache           => "There is no active tab to clear the cache.";
        public string Cfg_Msg_CacheDeleted               => "Cache cleared.";
        public string Cfg_Msg_HomePagesSaved             => "Home pages saved.";
        public string Cfg_Msg_SearchSaved                => "Search engine saved.";
        public string Cfg_Msg_ExportOk                   => "Settings exported successfully.";
        public string Cfg_Msg_ImportOk                   => "Settings imported successfully.\nRestart the application to apply all changes.";
        public string Cfg_Msg_ExportError                => "Export error: {0}";
        public string Cfg_Msg_ImportError                => "Import error: {0}";
        public string Cfg_Msg_Export                     => "Export";
        public string Cfg_Msg_Import                     => "Import";
        public string Cfg_Msg_Saved                      => "Saved";
        public string Cfg_Msg_Done                       => "Done";
        public string Cfg_Msg_Error                      => "Error";
        public string Cfg_Msg_Notice                     => "Notice";

        // =============================================
        // COOKIEMANAGERWINDOW
        // =============================================
        public string Cookie_WindowTitle     => "Cookie manager";
        public string Cookie_SearchDomain    => "Search domain:";
        public string Cookie_Reload          => "Reload";
        public string Cookie_Domains         => "Domains";
        public string Cookie_Cookies         => "Cookies";
        public string Cookie_SearchCookie    => "Search cookie:";
        public string Cookie_ColName         => "Name";
        public string Cookie_ColValue        => "Value";
        public string Cookie_ColPath         => "Path";
        public string Cookie_ColExpires      => "Expires";
        public string Cookie_Details         => "Details";
        public string Cookie_DetailName      => "Name:";
        public string Cookie_DetailValue     => "Value:";
        public string Cookie_DetailDomain    => "Domain:";
        public string Cookie_DetailPath      => "Path:";
        public string Cookie_DetailExpires   => "Expires:";
        public string Cookie_DetailFlags     => "Flags:";
        public string Cookie_FlagsFormat     => "Secure={0}, HttpOnly={1}, Session={2}";
        public string Cookie_CopyValue       => "Copy value";
        public string Cookie_EditCookie      => "Edit cookie";
        public string Cookie_DeleteCookie    => "Delete cookie";
        public string Cookie_DeleteDomain    => "Delete domain cookies";
        public string Cookie_DeleteAll       => "Delete ALL cookies";
        public string Cookie_Close           => "Close";

        // =============================================
        // EDITCOOKIEWINDOW
        // =============================================
        public string EditCookie_WindowTitle     => "Edit cookie";
        public string EditCookie_Name            => "Name:";
        public string EditCookie_Value           => "Value:";
        public string EditCookie_Path            => "Path:";
        public string EditCookie_Expires         => "Expiry:";
        public string EditCookie_Save            => "Save";
        public string EditCookie_Cancel          => "Cancel";
        public string EditCookie_Msg_EmptyValue  => "The cookie value cannot be empty.";
        public string EditCookie_Msg_EmptyPath   => "The path cannot be empty.";
        public string EditCookie_Msg_PathSlash   => "The path must start with '/'.";
        public string EditCookie_Msg_NoDate      => "You must select an expiry date.";
        public string EditCookie_Msg_PastDate    => "The expiry date cannot be in the past.";
        public string EditCookie_Msg_SaveError   => "Error saving cookie: {0}";

        // =============================================
        // FAVORITEEDITDIALOG
        // =============================================
        public string FavDlg_WindowTitle       => "Bookmark";
        public string FavDlg_Title             => "Title:";
        public string FavDlg_Url               => "URL:";
        public string FavDlg_Folder            => "Group/Folder:";
        public string FavDlg_Favicon           => "Favicon URL:";
        public string FavDlg_FaviconTooltip    => "Optional — leave empty to detect automatically";
        public string FavDlg_Save              => "Save";
        public string FavDlg_Cancel            => "Cancel";
        public string FavDlg_Msg_UrlRequired   => "URL is required.";
        public string FavDlg_Msg_Validation    => "Validation";

        // =============================================
        // FAVORITESWINDOW
        // =============================================
        public string Fav_WindowTitle        => "Bookmark manager";
        public string Fav_Add                => "➕ Add";
        public string Fav_Edit               => "✏ Edit";
        public string Fav_Delete             => "🗑 Delete";
        public string Fav_Up                 => "▲ Up";
        public string Fav_Down               => "▼ Down";
        public string Fav_Open               => "🌐 Open";
        public string Fav_OpenTooltip        => "Open in browser (or double-click the row)";
        public string Fav_CopyUrl            => "📋 Copy URL";
        public string Fav_Close              => "✖ Close";
        public string Fav_Export             => "Export:";
        public string Fav_ExportHtml         => "📄 HTML";
        public string Fav_ExportHtmlTooltip  => "Netscape Bookmark HTML — importable in Chrome, Edge, Firefox";
        public string Fav_ExportJson         => "💾 JSON";
        public string Fav_ExportCsv          => "📊 CSV";
        public string Fav_Import             => "Import:";
        public string Fav_ImportHtml         => "📂 HTML";
        public string Fav_ImportHtmlTooltip  => "Netscape Bookmark HTML (exported from any browser)";
        public string Fav_ImportJson         => "📂 JSON";
        public string Fav_ImportFrom         => "Import bookmarks from:";
        public string Fav_ImportBtn          => "Import";
        public string Fav_Search             => "🔍 Search:";
        public string Fav_Group              => "  Group:";
        public string Fav_Clear              => "Clear";
        public string Fav_AllGroups          => "(All)";
        public string Fav_ColTitle           => "Title";
        public string Fav_ColGroup           => "Group";
        public string Fav_CtxOpen            => "🌐 Open in browser";
        public string Fav_CtxEdit            => "✏ Edit";
        public string Fav_CtxCopy            => "📋 Copy URL";
        public string Fav_CtxDelete          => "🗑 Delete";
        public string Fav_Msg_Duplicate      => "A bookmark with that URL already exists.";
        public string Fav_Msg_DuplicateTitle => "Duplicate";
        public string Fav_Msg_ConfirmDelete  => "Delete {0} bookmark(s)?";
        public string Fav_Msg_Confirm        => "Confirm";
        public string Fav_Msg_ImportResult   => "Import complete:\n\n  ✔ Added:   {0}\n  ⏭ Skipped: {1} (duplicates)";
        public string Fav_Msg_ImportTitle    => "Import";
        public string Fav_Msg_ImportJsonError => "Error: {0}";
        public string Fav_Msg_ImportJsonTitle => "Import JSON";
        public string Fav_Msg_BrowserNotFound => "No bookmarks found for {0}.\n{1}";
        public string Fav_Msg_NotFound       => "Not found";
        public string Fav_Msg_FirefoxNotFound => "Firefox profile folder not found.";
        public string Fav_Msg_FirefoxFallback => "Cannot read Firefox's places.sqlite (Firefox may be running).\n\nAlternative: in Firefox go to\n  Bookmarks ▸ Manage bookmarks ▸ Import and backup ▸ Export bookmarks to HTML\nand use 'Import HTML' here.\n\nOpen Import HTML now?";
        public string Fav_Msg_FirefoxTitle   => "Firefox";
        public string Fav_Msg_SqliteRequired => "Install the NuGet package Microsoft.Data.Sqlite to read Firefox databases.";
        public string Fav_Msg_ErrorOpenUrl   => "Error opening URL";
        public string Fav_StatusReady        => "Ready";
        public string Fav_StatusAdded        => "Added: {0}";
        public string Fav_StatusEdited       => "Edited: {0}";
        public string Fav_StatusDeleted      => "Deleted: {0}";
        public string Fav_StatusUrlCopied    => "URL copied to clipboard";
        public string Fav_StatusExported     => "Exported {0} → {1}";
        public string Fav_StatusImported     => "Imported {0} bookmark(s) — {1} duplicate(s) skipped";
        public string Fav_Count              => "{0} / {1} bookmarks";

        // =============================================
        // HISTORYWINDOW
        // =============================================
        public string History_WindowTitle        => "Browsing history";
        public string History_Title              => "🕐 History";
        public string History_IncognitoNote      => "Sites visited in incognito mode do not appear here.";
        public string History_DeleteAll          => "🗑 Delete all";
        public string History_Today              => "Today";
        public string History_Yesterday          => "Yesterday";
        public string History_ThisWeek           => "This week";
        public string History_ThisMonth          => "This month";
        public string History_SearchResults      => "Results for \"{0}\"";
        public string History_Msg_ConfirmDelete  => "Delete all history?";
        public string History_Msg_Confirmation   => "Confirmation";

        // =============================================
        // MAINWINDOW
        // =============================================
        public string Main_MaxPanel1            => "Maximise panel 1";
        public string Main_MaxPanel2            => "Maximise panel 2";
        public string Main_MaxPanel3            => "Maximise panel 3";
        public string Main_MaxPanel4            => "Maximise panel 4";
        public string Main_RestorePanels        => "Restore all panels";
        public string Main_Notes                => "Notes";
        public string Main_WelcomePopup         => "⚙ Set up your home pages and more...";
        public string Main_FavAdd               => "Add to favorites";
        public string Main_FavManage            => "📁 Manage favorites";
        public string Main_MenuGeneral          => "General";
        public string Main_MenuApariencia       => "Appearance";
        public string Main_MenuFavoritos        => "Favorites";
        public string Main_MenuHistorial        => "History";
        public string Main_MenuPrivacidad       => "Privacy";
        public string Main_MenuPermisos         => "Permissions";
        public string Main_MenuCookies          => "Cookies";
        public string Main_MenuAvanzado         => "Advanced";
        public string Main_MenuAcercaDe         => "About...";
        public string Main_CtxClearMark         => "No mark";
        public string Main_CtxMarkColor         => "Mark with color";
        public string Main_CtxSortByColor       => "Sort by color";
        public string Main_CtxGoHome            => "Go to home page";
        public string Main_CtxIncognitoOn       => "Enable incognito mode";
        public string Main_CtxIncognitoOff      => "Disable incognito mode";
        public string Main_CtxReload            => "Reload page";
        public string Main_CtxOpenInWindow      => "Open in new window";
        public string Main_CtxOpenWith          => "Open with...";
        public string Main_CtxReopenClosed      => "Reopen closed tab";
        public string Main_CtxNewTab            => "New tab";
        public string Main_CtxNewIncognito      => "New incognito tab";
        public string Main_CtxMoveTab           => "Move tab to";
        public string Main_CtxCopyTab           => "Copy tab to";
        public string Main_CtxDuplicate         => "Duplicate tab (to the right)";
        public string Main_CtxClose             => "Close tabs";
        public string Main_CtxRestoreSession    => "Restore last session tabs";
        public string Main_PosRight             => "To the right";
        public string Main_PosLeft              => "To the left";
        public string Main_PosEnd               => "At the end";
        public string Main_PosStart             => "At the beginning";
        public string Main_CloseThis            => "This one";
        public string Main_CloseAllRight        => "All to the right";
        public string Main_CloseAllLeft         => "All to the left";
        public string Main_CloseOthers          => "All except this one";
        public string Main_Browser1             => "Browser 1";
        public string Main_Browser2             => "Browser 2";
        public string Main_Browser3             => "Browser 3";
        public string Main_Browser4             => "Browser 4";
        public string Main_PermAllow            => "Allow";
        public string Main_PermBlock            => "Block";
        public string Main_NotesReminderPrefix  => "You have ";
        public string Main_NotesReminderSuffix  => " note(s) with a pending reminder:\n\n";
        public string Main_NotesReminderQuestion => "\nDo you want to see the notes?";
        public string Main_NotesReminderTitle   => "Notes reminder";
        public string Main_NoSession            => "No saved session.";
        public string Main_RestoreSessionTitle  => "Restore session";
        public string Main_SecureNoInfo         => "No security information available.";
        public string Main_SecureYes            => "The connection is secure.\nThe site uses HTTPS.";
        public string Main_SecureNo             => "The connection is NOT secure.\nThe site does not use HTTPS.";
        public string Main_ErrExternalLink      => "Error opening external link: ";
        public string Main_NoTabInit            => "No tab has been initialised yet.";
        public string Main_ErrOpenBrowser       => "Could not open browser: ";
        public string Main_AboutTitle           => "About Multinavigator";
        public string Main_AboutNotAvailable    => "Not available";
        public string Main_AboutVersion         => "Ver: 7.0.0";
        public string Main_AboutChromium        => "Chromium Ver: ";
        public string Main_AboutLocalIp         => "🏠  Local IP: ";
        public string Main_AboutPublicIp        => "🌍  Public IP: ";
        public string Main_AboutAppName         => "Multinavigator 7";
        public string Main_PermFormat           => "{0} wants to use: {1}";
        public string Main_NewTab               => "New tab";
        public string Main_Loading              => "Loading...";

        // =============================================
        // NOTASWINDOW
        // =============================================
        public string Notas_WindowTitle         => "Notes";
        public string Notas_NewNote             => "+ New note";
        public string Notas_Reminder            => "Reminder";
        public string Notas_MsgDelete           => "Delete this note?";
        public string Notas_MsgConfirm          => "Confirm";

        // =============================================
        // THEMEEDITOR
        // =============================================
        public string Theme_WindowTitle         => "Custom Theme Editor";
        public string Theme_HeaderTitle         => "🎨 Theme Editor";
        public string Theme_PreviewTitle        => "Preview";
        public string Theme_PreviewTab1         => "Tab 1";
        public string Theme_PreviewTab2         => "Tab 2";
        public string Theme_PreviewIncognito    => "🕵️ Incognito";
        public string Theme_PreviewUrl          => "🔒 https://example.com";
        public string Theme_Hue                 => "🎨 Hue";
        public string Theme_Saturation          => "💧 Saturation";
        public string Theme_Luminosity          => "💡 Luminosity";
        public string Theme_SecGeneral          => "🎨 General Colors";
        public string Theme_SecTabsNormal       => "📑 Normal Tabs";
        public string Theme_SecNavBar           => "🧭 Navigation Bar";
        public string Theme_SecButtons          => "🔘 Buttons";
        public string Theme_SecButtonCentro     => "🔘 Center Button";
        public string Theme_SecIncognito        => "🕵️ Incognito Tabs";
        public string Theme_ColorWindowBg       => "Window background";
        public string Theme_ColorWindowFg       => "Window text";
        public string Theme_ColorTabInactive    => "Inactive tab";
        public string Theme_ColorTabActive      => "Active tab";
        public string Theme_ColorHover          => "Hover";
        public string Theme_ColorTabActiveHover => "Active + Hover";
        public string Theme_ColorTabText        => "Tab text";
        public string Theme_ColorNavBarBg       => "Bar background";
        public string Theme_ColorNavBarFg       => "Text";
        public string Theme_ColorUrlBg          => "URL background";
        public string Theme_ColorUrlFg          => "URL text";
        public string Theme_ColorButtonAccent   => "Button color";
        public string Theme_ColorButtonPressed  => "Pressed";
        public string Theme_ColorButtonCentro   => "Center button color";
        public string Theme_ColorIncogInactive  => "Inactive";
        public string Theme_ColorIncogActive    => "Active";
        public string Theme_ColorIncogText      => "Text";
        public string Theme_LabelName           => "Name:";
        public string Theme_BtnSave             => "💾 Save";
        public string Theme_BtnCancel           => "❌ Cancel";
        public string Theme_PickerTitle         => "Color picker";
        public string Theme_PickerHue           => "Hue (H)";
        public string Theme_PickerSaturation    => "Saturation (S)";
        public string Theme_PickerLuminosity    => "Luminosity (L)";
        public string Theme_PickerOk            => "OK";
        public string Theme_PickerCancel        => "Cancel";
        public string Theme_MsgInitError        => "Error initialising the editor: ";
        public string Theme_MsgInitErrorTitle   => "Error";
        public string Theme_MsgNoName           => "Please enter a name for the theme.";
        public string Theme_MsgNoNameTitle      => "Name required";
        public string Theme_MsgPredefined       => "is a predefined theme and cannot be overwritten.\nChoose a different name.";
        public string Theme_MsgPredefinedTitle  => "Reserved name";
        public string Theme_MsgOverwriteFormat  => "A custom theme named '{0}' already exists.\nDo you want to overwrite it?";
        public string Theme_MsgOverwriteTitle   => "Existing theme";
        public string Theme_MsgSavedFormat      => "Theme '{0}' saved successfully.";
        public string Theme_MsgSavedTitle       => "Theme saved";
    }
}
