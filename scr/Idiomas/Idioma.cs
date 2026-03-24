// Resources/Idioma.Instance.cs
// Fachada de localización + contrato de interfaz.
// Para añadir un idioma: crear IdiomaXx.cs que implemente IIdiomaStrings
// y añadir UN caso más en SetLanguage().

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Multinavigator.Idiomas
{
    // ── Contrato que debe implementar cada clase de idioma ───────────────
    internal interface IIdiomaStrings
    {
        string About_Github_button { get; }
        string About_Github { get; }
        // CONFIGURACION
        string Cfg_WindowTitle { get; }
        string Cfg_Title { get; }
        string Cfg_Language_Label { get; }
        string Cfg_Language_Title { get; }
        string Cfg_Language_RestartHint { get; }
        string Cfg_TabGeneral { get; }
        string Cfg_TabApariencia { get; }
        string Cfg_TabPrivacidad { get; }
        string Cfg_TabPermisos { get; }
        string Cfg_TabAvanzado { get; }
        string Cfg_HomePages_Title { get; }
        string Cfg_HomePages_Desc { get; }
        string Cfg_HomePages_Panel1 { get; }
        string Cfg_HomePages_Panel2 { get; }
        string Cfg_HomePages_Panel3 { get; }
        string Cfg_HomePages_Panel4 { get; }
        string Cfg_HomePages_Save { get; }
        string Cfg_Session_Title { get; }
        string Cfg_Session_Restore { get; }
        string Cfg_Search_Title { get; }
        string Cfg_Search_Desc { get; }
        string Cfg_Search_Save { get; }
        string Cfg_Search_CustomUrl { get; }
        string Cfg_Backup_Title { get; }
        string Cfg_Backup_Desc { get; }
        string Cfg_Backup_Favorites { get; }
        string Cfg_Backup_Themes { get; }
        string Cfg_Backup_Permissions { get; }
        string Cfg_Backup_Settings { get; }
        string Cfg_Backup_History { get; }
        string Cfg_Backup_Export { get; }
        string Cfg_Backup_Import { get; }
        string Cfg_Backup_NoCookies { get; }
        string Cfg_Appearance_Themes_Title { get; }
        string Cfg_Appearance_Colors_Title { get; }
        string Cfg_Appearance_OpenEditor { get; }
        string Cfg_Appearance_EditorDesc { get; }
        string Cfg_Appearance_Preview { get; }
        string Cfg_Appearance_Tab1 { get; }
        string Cfg_Appearance_Tab2 { get; }
        string Cfg_Appearance_Incognito { get; }
        string Cfg_Appearance_Options { get; }
        string Cfg_Appearance_DarkIncognito { get; }
        string Cfg_Privacy_Title { get; }
        string Cfg_Privacy_Cookies { get; }
        string Cfg_Privacy_CookiesUnit { get; }
        string Cfg_Privacy_CookiesManage { get; }
        string Cfg_Privacy_DeleteAll { get; }
        string Cfg_Privacy_History { get; }
        string Cfg_Privacy_HistoryUnit { get; }
        string Cfg_Privacy_HistoryView { get; }
        string Cfg_Privacy_Cache { get; }
        string Cfg_Privacy_CacheUnit { get; }
        string Cfg_Privacy_CacheDelete { get; }
        string Cfg_Perms_GlobalTitle { get; }
        string Cfg_Perms_DomainTitle { get; }
        string Cfg_Perms_Camera { get; }
        string Cfg_Perms_Mic { get; }
        string Cfg_Perms_Location { get; }
        string Cfg_Perms_Notifications { get; }
        string Cfg_Perms_DomainCol { get; }
        string Cfg_Perms_AddDomain { get; }
        string Cfg_Perms_Reset { get; }
        string Cfg_Adv_Title { get; }
        string Cfg_Adv_Warning { get; }
        string Cfg_Adv_BgNetworking { get; }
        string Cfg_Adv_BgNetworking_Desc { get; }
        string Cfg_Adv_Sync { get; }
        string Cfg_Adv_Sync_Desc { get; }
        string Cfg_Adv_Translate { get; }
        string Cfg_Adv_Translate_Desc { get; }
        string Cfg_Adv_Extensions { get; }
        string Cfg_Adv_Extensions_Desc { get; }
        string Cfg_Adv_DefaultApps { get; }
        string Cfg_Adv_DefaultApps_Desc { get; }
        string Cfg_Adv_DefaultBrowserCheck { get; }
        string Cfg_Adv_DefaultBrowserCheck_Desc { get; }
        string Cfg_Adv_Metrics { get; }
        string Cfg_Adv_Metrics_Desc { get; }
        string Cfg_Adv_Breakpad { get; }
        string Cfg_Adv_Breakpad_Desc { get; }
        string Cfg_Adv_Phishing { get; }
        string Cfg_Adv_Phishing_Desc { get; }
        string Cfg_Adv_HangMonitor { get; }
        string Cfg_Adv_HangMonitor_Desc { get; }
        string Cfg_Adv_Repost { get; }
        string Cfg_Adv_Repost_Desc { get; }
        string Cfg_Adv_DomainReliability { get; }
        string Cfg_Adv_DomainReliability_Desc { get; }
        string Cfg_Adv_ComponentUpdate { get; }
        string Cfg_Adv_ComponentUpdate_Desc { get; }
        string Cfg_Adv_BgTimer { get; }
        string Cfg_Adv_BgTimer_Desc { get; }
        string Cfg_Adv_RendererBg { get; }
        string Cfg_Adv_RendererBg_Desc { get; }
        string Cfg_Adv_IpcFlood { get; }
        string Cfg_Adv_IpcFlood_Desc { get; }
        string Cfg_Adv_StateOn { get; }
        string Cfg_Adv_StateOff { get; }
        string Cfg_Msg_ConfirmDeleteTheme { get; }
        string Cfg_Msg_ConfirmDeleteThemeTitle { get; }
        string Cfg_Msg_NoTabInitialized { get; }
        string Cfg_Msg_ConfirmDeleteHistory { get; }
        string Cfg_Msg_ConfirmDeleteCookies { get; }
        string Cfg_Msg_Confirmation { get; }
        string Cfg_Msg_CookiesDeleted { get; }
        string Cfg_Msg_NoActiveTabCache { get; }
        string Cfg_Msg_CacheDeleted { get; }
        string Cfg_Msg_HomePagesSaved { get; }
        string Cfg_Msg_SearchSaved { get; }
        string Cfg_Msg_ExportOk { get; }
        string Cfg_Msg_ImportOk { get; }
        string Cfg_Msg_ExportError { get; }
        string Cfg_Msg_ImportError { get; }
        string Cfg_Msg_Export { get; }
        string Cfg_Msg_Import { get; }
        string Cfg_Msg_Saved { get; }
        string Cfg_Msg_Done { get; }
        string Cfg_Msg_Error { get; }
        string Cfg_Msg_Notice { get; }
        // COOKIEMANAGERWINDOW
        string Cookie_WindowTitle { get; }
        string Cookie_SearchDomain { get; }
        string Cookie_Reload { get; }
        string Cookie_Domains { get; }
        string Cookie_Cookies { get; }
        string Cookie_SearchCookie { get; }
        string Cookie_ColName { get; }
        string Cookie_ColValue { get; }
        string Cookie_ColPath { get; }
        string Cookie_ColExpires { get; }
        string Cookie_Details { get; }
        string Cookie_DetailName { get; }
        string Cookie_DetailValue { get; }
        string Cookie_DetailDomain { get; }
        string Cookie_DetailPath { get; }
        string Cookie_DetailExpires { get; }
        string Cookie_DetailFlags { get; }
        string Cookie_FlagsFormat { get; }
        string Cookie_CopyValue { get; }
        string Cookie_EditCookie { get; }
        string Cookie_DeleteCookie { get; }
        string Cookie_DeleteDomain { get; }
        string Cookie_DeleteAll { get; }
        string Cookie_Close { get; }
        // EDITCOOKIEWINDOW
        string EditCookie_WindowTitle { get; }
        string EditCookie_Name { get; }
        string EditCookie_Value { get; }
        string EditCookie_Path { get; }
        string EditCookie_Expires { get; }
        string EditCookie_Save { get; }
        string EditCookie_Cancel { get; }
        string EditCookie_Msg_EmptyValue { get; }
        string EditCookie_Msg_EmptyPath { get; }
        string EditCookie_Msg_PathSlash { get; }
        string EditCookie_Msg_NoDate { get; }
        string EditCookie_Msg_PastDate { get; }
        string EditCookie_Msg_SaveError { get; }
        // FAVORITEEDITDIALOG
        string FavDlg_WindowTitle { get; }
        string FavDlg_Title { get; }
        string FavDlg_Url { get; }
        string FavDlg_Folder { get; }
        string FavDlg_Favicon { get; }
        string FavDlg_FaviconTooltip { get; }
        string FavDlg_Save { get; }
        string FavDlg_Cancel { get; }
        string FavDlg_Msg_UrlRequired { get; }
        string FavDlg_Msg_Validation { get; }
        // FAVORITESWINDOW
        string Fav_WindowTitle { get; }
        string Fav_Add { get; }
        string Fav_Edit { get; }
        string Fav_Delete { get; }
        string Fav_Up { get; }
        string Fav_Down { get; }
        string Fav_Open { get; }
        string Fav_OpenTooltip { get; }
        string Fav_CopyUrl { get; }
        string Fav_Close { get; }
        string Fav_Export { get; }
        string Fav_ExportHtml { get; }
        string Fav_ExportHtmlTooltip { get; }
        string Fav_ExportJson { get; }
        string Fav_ExportCsv { get; }
        string Fav_Import { get; }
        string Fav_ImportHtml { get; }
        string Fav_ImportHtmlTooltip { get; }
        string Fav_ImportJson { get; }
        string Fav_ImportFrom { get; }
        string Fav_ImportBtn { get; }
        string Fav_Search { get; }
        string Fav_Group { get; }
        string Fav_Clear { get; }
        string Fav_AllGroups { get; }
        string Fav_ColTitle { get; }
        string Fav_ColGroup { get; }
        string Fav_CtxOpen { get; }
        string Fav_CtxEdit { get; }
        string Fav_CtxCopy { get; }
        string Fav_CtxDelete { get; }
        string Fav_Msg_Duplicate { get; }
        string Fav_Msg_DuplicateTitle { get; }
        string Fav_Msg_ConfirmDelete { get; }
        string Fav_Msg_Confirm { get; }
        string Fav_Msg_ImportResult { get; }
        string Fav_Msg_ImportTitle { get; }
        string Fav_Msg_ImportJsonError { get; }
        string Fav_Msg_ImportJsonTitle { get; }
        string Fav_Msg_BrowserNotFound { get; }
        string Fav_Msg_NotFound { get; }
        string Fav_Msg_FirefoxNotFound { get; }
        string Fav_Msg_FirefoxFallback { get; }
        string Fav_Msg_FirefoxTitle { get; }
        string Fav_Msg_SqliteRequired { get; }
        string Fav_Msg_ErrorOpenUrl { get; }
        string Fav_StatusReady { get; }
        string Fav_StatusAdded { get; }
        string Fav_StatusEdited { get; }
        string Fav_StatusDeleted { get; }
        string Fav_StatusUrlCopied { get; }
        string Fav_StatusExported { get; }
        string Fav_StatusImported { get; }
        string Fav_Count { get; }
        // HISTORYWINDOW
        string History_WindowTitle { get; }
        string History_Title { get; }
        string History_IncognitoNote { get; }
        string History_DeleteAll { get; }
        string History_Today { get; }
        string History_Yesterday { get; }
        string History_ThisWeek { get; }
        string History_ThisMonth { get; }
        string History_SearchResults { get; }
        string History_Msg_ConfirmDelete { get; }
        string History_Msg_Confirmation { get; }
        // MAINWINDOW
        string Main_MaxPanel1 { get; }
        string Main_MaxPanel2 { get; }
        string Main_MaxPanel3 { get; }
        string Main_MaxPanel4 { get; }
        string Main_RestorePanels { get; }
        string Main_Notes { get; }
        string Main_WelcomePopup { get; }
        string Main_FavAdd { get; }
        string Main_FavManage { get; }
        string Main_MenuGeneral { get; }
        string Main_MenuApariencia { get; }
        string Main_MenuFavoritos { get; }
        string Main_MenuHistorial { get; }
        string Main_MenuPrivacidad { get; }
        string Main_MenuPermisos { get; }
        string Main_MenuCookies { get; }
        string Main_MenuAvanzado { get; }
        string Main_MenuAcercaDe { get; }
        string Main_CtxClearMark { get; }
        string Main_CtxMarkColor { get; }
        string Main_CtxSortByColor { get; }
        string Main_CtxGoHome { get; }
        string Main_CtxIncognitoOn { get; }
        string Main_CtxIncognitoOff { get; }
        string Main_CtxReload { get; }
        string Main_CtxOpenInWindow { get; }
        string Main_CtxOpenWith { get; }
        string Main_CtxReopenClosed { get; }
        string Main_CtxNewTab { get; }
        string Main_CtxNewIncognito { get; }
        string Main_CtxMoveTab { get; }
        string Main_CtxCopyTab { get; }
        string Main_CtxDuplicate { get; }
        string Main_CtxClose { get; }
        string Main_CtxRestoreSession { get; }
        string Main_PosRight { get; }
        string Main_PosLeft { get; }
        string Main_PosEnd { get; }
        string Main_PosStart { get; }
        string Main_CloseThis { get; }
        string Main_CloseAllRight { get; }
        string Main_CloseAllLeft { get; }
        string Main_CloseOthers { get; }
        string Main_Browser1 { get; }
        string Main_Browser2 { get; }
        string Main_Browser3 { get; }
        string Main_Browser4 { get; }
        string Main_PermAllow { get; }
        string Main_PermBlock { get; }
        string Main_NotesReminderPrefix { get; }
        string Main_NotesReminderSuffix { get; }
        string Main_NotesReminderQuestion { get; }
        string Main_NotesReminderTitle { get; }
        string Main_NoSession { get; }
        string Main_RestoreSessionTitle { get; }
        string Main_SecureNoInfo { get; }
        string Main_SecureYes { get; }
        string Main_SecureNo { get; }
        string Main_ErrExternalLink { get; }
        string Main_NoTabInit { get; }
        string Main_ErrOpenBrowser { get; }
        string Main_AboutTitle { get; }
        string Main_AboutNotAvailable { get; }
        string Main_AboutVersion { get; }
        string Main_AboutChromium { get; }
        string Main_AboutLocalIp { get; }
        string Main_AboutPublicIp { get; }
        string Main_AboutAppName { get; }
        string Main_PermFormat { get; }
        string Main_NewTab { get; }
        string Main_Loading { get; }
        // NOTASWINDOW
        string Notas_WindowTitle { get; }
        string Notas_NewNote { get; }
        string Notas_Reminder { get; }
        string Notas_MsgDelete { get; }
        string Notas_MsgConfirm { get; }
        // THEMEEDITOR
        string Theme_ColorDarkWebContent { get; }
        string Theme_WindowTitle { get; }
        string Theme_HeaderTitle { get; }
        string Theme_PreviewTitle { get; }
        string Theme_PreviewTab1 { get; }
        string Theme_PreviewTab2 { get; }
        string Theme_PreviewIncognito { get; }
        string Theme_PreviewUrl { get; }
        string Theme_Hue { get; }
        string Theme_Saturation { get; }
        string Theme_Luminosity { get; }
        string Theme_SecGeneral { get; }
        string Theme_SecTabsNormal { get; }
        string Theme_SecNavBar { get; }
        string Theme_SecButtons { get; }
        string Theme_SecButtonCentro { get; }
        string Theme_SecIncognito { get; }
        string Theme_ColorWindowBg { get; }
        string Theme_ColorWindowFg { get; }
        string Theme_ColorTabInactive { get; }
        string Theme_ColorTabActive { get; }
        string Theme_ColorHover { get; }
        string Theme_ColorTabActiveHover { get; }
        string Theme_ColorTabText { get; }
        string Theme_ColorNavBarBg { get; }
        string Theme_ColorNavBarFg { get; }
        string Theme_ColorUrlBg { get; }
        string Theme_ColorUrlFg { get; }
        string Theme_ColorButtonAccent { get; }
        string Theme_ColorButtonPressed { get; }
        string Theme_ColorButtonCentro { get; }
        string Theme_ColorIncogInactive { get; }
        string Theme_ColorIncogActive { get; }
        string Theme_ColorIncogText { get; }
        string Theme_LabelName { get; }
        string Theme_BtnSave { get; }
        string Theme_BtnCancel { get; }
        string Theme_PickerTitle { get; }
        string Theme_PickerHue { get; }
        string Theme_PickerSaturation { get; }
        string Theme_PickerLuminosity { get; }
        string Theme_PickerOk { get; }
        string Theme_PickerCancel { get; }
        string Theme_MsgInitError { get; }
        string Theme_MsgInitErrorTitle { get; }
        string Theme_MsgNoName { get; }
        string Theme_MsgNoNameTitle { get; }
        string Theme_MsgPredefined { get; }
        string Theme_MsgPredefinedTitle { get; }
        string Theme_MsgOverwriteFormat { get; }
        string Theme_MsgOverwriteTitle { get; }
        string Theme_MsgSavedFormat { get; }
        string Theme_MsgSavedTitle { get; }
    }

    // ── Fachada pública ──────────────────────────────────────────────────
    public class Idioma : INotifyPropertyChanged
    {
        public string About_Github_button => _L.About_Github_button;
        public string About_Github => _L.About_Github;
        // 1. Instancia única (Singleton)
        private static Idioma _instance;
        public static Idioma Instance
        {
            get
            {
                // Si nadie la ha creado, la creamos nosotros
                if (_instance == null) _instance = new Idioma();
                return _instance;
            }
        }

        // 2. Constructor MODIFICADO
        public Idioma()
        {
            // Esto es la clave: Cuando App.xaml crea su versión de "Idioma",
            // le decimos que esa sea la instancia oficial para toda la App.
            _instance = this;
        }

        // --- A partir de aquí, deja tu código tal cual estaba ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string CurrentLanguage { get; private set; } = "es";
        private static IIdiomaStrings _L = IdiomaEs.Instance;

        public void SetLanguage(string code)
        {
            CurrentLanguage = code;
            _L = code switch
            {
                "es" => IdiomaEs.Instance,
                "en" => IdiomaEn.Instance,
                "zh" => IdiomaZh.Instance,
                "hi" => IdiomaHi.Instance,
                "nl" => IdiomaNl.Instance,
                "pt" => IdiomaPt.Instance,
                "fr" => IdiomaFr.Instance,
                "de" => IdiomaDe.Instance,
                "ja" => IdiomaJa.Instance,
                "ru" => IdiomaRu.Instance,
                "ko" => IdiomaKo.Instance,
                "tr" => IdiomaTr.Instance,
                "id" => IdiomaId.Instance,
                "it" => IdiomaIt.Instance,
                "bn" => IdiomaBn.Instance,
                "vi" => IdiomaVi.Instance,
                "pl" => IdiomaPl.Instance,
                "th" => IdiomaTh.Instance,
                "sw" => IdiomaSw.Instance,
                "tl" => IdiomaTl.Instance,
                "uk" => IdiomaUk.Instance,
                "cs" => IdiomaCs.Instance,
                "ro" => IdiomaRo.Instance,
                "ms" => IdiomaMs.Instance,
                "ur" => IdiomaUr.Instance,
                _ => IdiomaEs.Instance
            };

            OnPropertyChanged(null);
        }

        // Cfg_Language_Label es especial: siempre añade (Language) al final
        public string Cfg_Language_Label => $"{_L.Cfg_Language_Label} (Language)";

        // ── CONFIGURACION ──
        public string Cfg_WindowTitle => _L.Cfg_WindowTitle;
        public string Cfg_Title => _L.Cfg_Title;
        public string Cfg_Language_Title => _L.Cfg_Language_Title;
        public string Cfg_Language_RestartHint => _L.Cfg_Language_RestartHint;
        public string Cfg_TabGeneral => _L.Cfg_TabGeneral;
        public string Cfg_TabApariencia => _L.Cfg_TabApariencia;
        public string Cfg_TabPrivacidad => _L.Cfg_TabPrivacidad;
        public string Cfg_TabPermisos => _L.Cfg_TabPermisos;
        public string Cfg_TabAvanzado => _L.Cfg_TabAvanzado;
        public string Cfg_HomePages_Title => _L.Cfg_HomePages_Title;
        public string Cfg_HomePages_Desc => _L.Cfg_HomePages_Desc;
        public string Cfg_HomePages_Panel1 => _L.Cfg_HomePages_Panel1;
        public string Cfg_HomePages_Panel2 => _L.Cfg_HomePages_Panel2;
        public string Cfg_HomePages_Panel3 => _L.Cfg_HomePages_Panel3;
        public string Cfg_HomePages_Panel4 => _L.Cfg_HomePages_Panel4;
        public string Cfg_HomePages_Save => _L.Cfg_HomePages_Save;
        public string Cfg_Session_Title => _L.Cfg_Session_Title;
        public string Cfg_Session_Restore => _L.Cfg_Session_Restore;
        public string Cfg_Search_Title => _L.Cfg_Search_Title;
        public string Cfg_Search_Desc => _L.Cfg_Search_Desc;
        public string Cfg_Search_Save => _L.Cfg_Search_Save;
        public string Cfg_Search_CustomUrl => _L.Cfg_Search_CustomUrl;
        public string Cfg_Backup_Title => _L.Cfg_Backup_Title;
        public string Cfg_Backup_Desc => _L.Cfg_Backup_Desc;
        public string Cfg_Backup_Favorites => _L.Cfg_Backup_Favorites;
        public string Cfg_Backup_Themes => _L.Cfg_Backup_Themes;
        public string Cfg_Backup_Permissions => _L.Cfg_Backup_Permissions;
        public string Cfg_Backup_Settings => _L.Cfg_Backup_Settings;
        public string Cfg_Backup_History => _L.Cfg_Backup_History;
        public string Cfg_Backup_Export => _L.Cfg_Backup_Export;
        public string Cfg_Backup_Import => _L.Cfg_Backup_Import;
        public string Cfg_Backup_NoCookies => _L.Cfg_Backup_NoCookies;
        public string Cfg_Appearance_Themes_Title => _L.Cfg_Appearance_Themes_Title;
        public string Cfg_Appearance_Colors_Title => _L.Cfg_Appearance_Colors_Title;
        public string Cfg_Appearance_OpenEditor => _L.Cfg_Appearance_OpenEditor;
        public string Cfg_Appearance_EditorDesc => _L.Cfg_Appearance_EditorDesc;
        public string Cfg_Appearance_Preview => _L.Cfg_Appearance_Preview;
        public string Cfg_Appearance_Tab1 => _L.Cfg_Appearance_Tab1;
        public string Cfg_Appearance_Tab2 => _L.Cfg_Appearance_Tab2;
        public string Cfg_Appearance_Incognito => _L.Cfg_Appearance_Incognito;
        public string Cfg_Appearance_Options => _L.Cfg_Appearance_Options;
        public string Cfg_Appearance_DarkIncognito => _L.Cfg_Appearance_DarkIncognito;
        public string Cfg_Privacy_Title => _L.Cfg_Privacy_Title;
        public string Cfg_Privacy_Cookies => _L.Cfg_Privacy_Cookies;
        public string Cfg_Privacy_CookiesUnit => _L.Cfg_Privacy_CookiesUnit;
        public string Cfg_Privacy_CookiesManage => _L.Cfg_Privacy_CookiesManage;
        public string Cfg_Privacy_DeleteAll => _L.Cfg_Privacy_DeleteAll;
        public string Cfg_Privacy_History => _L.Cfg_Privacy_History;
        public string Cfg_Privacy_HistoryUnit => _L.Cfg_Privacy_HistoryUnit;
        public string Cfg_Privacy_HistoryView => _L.Cfg_Privacy_HistoryView;
        public string Cfg_Privacy_Cache => _L.Cfg_Privacy_Cache;
        public string Cfg_Privacy_CacheUnit => _L.Cfg_Privacy_CacheUnit;
        public string Cfg_Privacy_CacheDelete => _L.Cfg_Privacy_CacheDelete;
        public string Cfg_Perms_GlobalTitle => _L.Cfg_Perms_GlobalTitle;
        public string Cfg_Perms_DomainTitle => _L.Cfg_Perms_DomainTitle;
        public string Cfg_Perms_Camera => _L.Cfg_Perms_Camera;
        public string Cfg_Perms_Mic => _L.Cfg_Perms_Mic;
        public string Cfg_Perms_Location => _L.Cfg_Perms_Location;
        public string Cfg_Perms_Notifications => _L.Cfg_Perms_Notifications;
        public string Cfg_Perms_DomainCol => _L.Cfg_Perms_DomainCol;
        public string Cfg_Perms_AddDomain => _L.Cfg_Perms_AddDomain;
        public string Cfg_Perms_Reset => _L.Cfg_Perms_Reset;
        public string Cfg_Adv_Title => _L.Cfg_Adv_Title;
        public string Cfg_Adv_Warning => _L.Cfg_Adv_Warning;
        public string Cfg_Adv_BgNetworking => _L.Cfg_Adv_BgNetworking;
        public string Cfg_Adv_BgNetworking_Desc => _L.Cfg_Adv_BgNetworking_Desc;
        public string Cfg_Adv_Sync => _L.Cfg_Adv_Sync;
        public string Cfg_Adv_Sync_Desc => _L.Cfg_Adv_Sync_Desc;
        public string Cfg_Adv_Translate => _L.Cfg_Adv_Translate;
        public string Cfg_Adv_Translate_Desc => _L.Cfg_Adv_Translate_Desc;
        public string Cfg_Adv_Extensions => _L.Cfg_Adv_Extensions;
        public string Cfg_Adv_Extensions_Desc => _L.Cfg_Adv_Extensions_Desc;
        public string Cfg_Adv_DefaultApps => _L.Cfg_Adv_DefaultApps;
        public string Cfg_Adv_DefaultApps_Desc => _L.Cfg_Adv_DefaultApps_Desc;
        public string Cfg_Adv_DefaultBrowserCheck => _L.Cfg_Adv_DefaultBrowserCheck;
        public string Cfg_Adv_DefaultBrowserCheck_Desc => _L.Cfg_Adv_DefaultBrowserCheck_Desc;
        public string Cfg_Adv_Metrics => _L.Cfg_Adv_Metrics;
        public string Cfg_Adv_Metrics_Desc => _L.Cfg_Adv_Metrics_Desc;
        public string Cfg_Adv_Breakpad => _L.Cfg_Adv_Breakpad;
        public string Cfg_Adv_Breakpad_Desc => _L.Cfg_Adv_Breakpad_Desc;
        public string Cfg_Adv_Phishing => _L.Cfg_Adv_Phishing;
        public string Cfg_Adv_Phishing_Desc => _L.Cfg_Adv_Phishing_Desc;
        public string Cfg_Adv_HangMonitor => _L.Cfg_Adv_HangMonitor;
        public string Cfg_Adv_HangMonitor_Desc => _L.Cfg_Adv_HangMonitor_Desc;
        public string Cfg_Adv_Repost => _L.Cfg_Adv_Repost;
        public string Cfg_Adv_Repost_Desc => _L.Cfg_Adv_Repost_Desc;
        public string Cfg_Adv_DomainReliability => _L.Cfg_Adv_DomainReliability;
        public string Cfg_Adv_DomainReliability_Desc => _L.Cfg_Adv_DomainReliability_Desc;
        public string Cfg_Adv_ComponentUpdate => _L.Cfg_Adv_ComponentUpdate;
        public string Cfg_Adv_ComponentUpdate_Desc => _L.Cfg_Adv_ComponentUpdate_Desc;
        public string Cfg_Adv_BgTimer => _L.Cfg_Adv_BgTimer;
        public string Cfg_Adv_BgTimer_Desc => _L.Cfg_Adv_BgTimer_Desc;
        public string Cfg_Adv_RendererBg => _L.Cfg_Adv_RendererBg;
        public string Cfg_Adv_RendererBg_Desc => _L.Cfg_Adv_RendererBg_Desc;
        public string Cfg_Adv_IpcFlood => _L.Cfg_Adv_IpcFlood;
        public string Cfg_Adv_IpcFlood_Desc => _L.Cfg_Adv_IpcFlood_Desc;
        public string Cfg_Adv_StateOn => _L.Cfg_Adv_StateOn;
        public string Cfg_Adv_StateOff => _L.Cfg_Adv_StateOff;
        public string Cfg_Msg_ConfirmDeleteTheme => _L.Cfg_Msg_ConfirmDeleteTheme;
        public string Cfg_Msg_ConfirmDeleteThemeTitle => _L.Cfg_Msg_ConfirmDeleteThemeTitle;
        public string Cfg_Msg_NoTabInitialized => _L.Cfg_Msg_NoTabInitialized;
        public string Cfg_Msg_ConfirmDeleteHistory => _L.Cfg_Msg_ConfirmDeleteHistory;
        public string Cfg_Msg_ConfirmDeleteCookies => _L.Cfg_Msg_ConfirmDeleteCookies;
        public string Cfg_Msg_Confirmation => _L.Cfg_Msg_Confirmation;
        public string Cfg_Msg_CookiesDeleted => _L.Cfg_Msg_CookiesDeleted;
        public string Cfg_Msg_NoActiveTabCache => _L.Cfg_Msg_NoActiveTabCache;
        public string Cfg_Msg_CacheDeleted => _L.Cfg_Msg_CacheDeleted;
        public string Cfg_Msg_HomePagesSaved => _L.Cfg_Msg_HomePagesSaved;
        public string Cfg_Msg_SearchSaved => _L.Cfg_Msg_SearchSaved;
        public string Cfg_Msg_ExportOk => _L.Cfg_Msg_ExportOk;
        public string Cfg_Msg_ImportOk => _L.Cfg_Msg_ImportOk;
        public string Cfg_Msg_ExportError => _L.Cfg_Msg_ExportError;
        public string Cfg_Msg_ImportError => _L.Cfg_Msg_ImportError;
        public string Cfg_Msg_Export => _L.Cfg_Msg_Export;
        public string Cfg_Msg_Import => _L.Cfg_Msg_Import;
        public string Cfg_Msg_Saved => _L.Cfg_Msg_Saved;
        public string Cfg_Msg_Done => _L.Cfg_Msg_Done;
        public string Cfg_Msg_Error => _L.Cfg_Msg_Error;
        public string Cfg_Msg_Notice => _L.Cfg_Msg_Notice;
        // ── COOKIEMANAGERWINDOW ──
        public string Cookie_WindowTitle => _L.Cookie_WindowTitle;
        public string Cookie_SearchDomain => _L.Cookie_SearchDomain;
        public string Cookie_Reload => _L.Cookie_Reload;
        public string Cookie_Domains => _L.Cookie_Domains;
        public string Cookie_Cookies => _L.Cookie_Cookies;
        public string Cookie_SearchCookie => _L.Cookie_SearchCookie;
        public string Cookie_ColName => _L.Cookie_ColName;
        public string Cookie_ColValue => _L.Cookie_ColValue;
        public string Cookie_ColPath => _L.Cookie_ColPath;
        public string Cookie_ColExpires => _L.Cookie_ColExpires;
        public string Cookie_Details => _L.Cookie_Details;
        public string Cookie_DetailName => _L.Cookie_DetailName;
        public string Cookie_DetailValue => _L.Cookie_DetailValue;
        public string Cookie_DetailDomain => _L.Cookie_DetailDomain;
        public string Cookie_DetailPath => _L.Cookie_DetailPath;
        public string Cookie_DetailExpires => _L.Cookie_DetailExpires;
        public string Cookie_DetailFlags => _L.Cookie_DetailFlags;
        public string Cookie_FlagsFormat => _L.Cookie_FlagsFormat;
        public string Cookie_CopyValue => _L.Cookie_CopyValue;
        public string Cookie_EditCookie => _L.Cookie_EditCookie;
        public string Cookie_DeleteCookie => _L.Cookie_DeleteCookie;
        public string Cookie_DeleteDomain => _L.Cookie_DeleteDomain;
        public string Cookie_DeleteAll => _L.Cookie_DeleteAll;
        public string Cookie_Close => _L.Cookie_Close;
        // ── EDITCOOKIEWINDOW ──
        public string EditCookie_WindowTitle => _L.EditCookie_WindowTitle;
        public string EditCookie_Name => _L.EditCookie_Name;
        public string EditCookie_Value => _L.EditCookie_Value;
        public string EditCookie_Path => _L.EditCookie_Path;
        public string EditCookie_Expires => _L.EditCookie_Expires;
        public string EditCookie_Save => _L.EditCookie_Save;
        public string EditCookie_Cancel => _L.EditCookie_Cancel;
        public string EditCookie_Msg_EmptyValue => _L.EditCookie_Msg_EmptyValue;
        public string EditCookie_Msg_EmptyPath => _L.EditCookie_Msg_EmptyPath;
        public string EditCookie_Msg_PathSlash => _L.EditCookie_Msg_PathSlash;
        public string EditCookie_Msg_NoDate => _L.EditCookie_Msg_NoDate;
        public string EditCookie_Msg_PastDate => _L.EditCookie_Msg_PastDate;
        public string EditCookie_Msg_SaveError => _L.EditCookie_Msg_SaveError;
        // ── FAVORITEEDITDIALOG ──
        public string FavDlg_WindowTitle => _L.FavDlg_WindowTitle;
        public string FavDlg_Title => _L.FavDlg_Title;
        public string FavDlg_Url => _L.FavDlg_Url;
        public string FavDlg_Folder => _L.FavDlg_Folder;
        public string FavDlg_Favicon => _L.FavDlg_Favicon;
        public string FavDlg_FaviconTooltip => _L.FavDlg_FaviconTooltip;
        public string FavDlg_Save => _L.FavDlg_Save;
        public string FavDlg_Cancel => _L.FavDlg_Cancel;
        public string FavDlg_Msg_UrlRequired => _L.FavDlg_Msg_UrlRequired;
        public string FavDlg_Msg_Validation => _L.FavDlg_Msg_Validation;
        // ── FAVORITESWINDOW ──
        public string Fav_WindowTitle => _L.Fav_WindowTitle;
        public string Fav_Add => _L.Fav_Add;
        public string Fav_Edit => _L.Fav_Edit;
        public string Fav_Delete => _L.Fav_Delete;
        public string Fav_Up => _L.Fav_Up;
        public string Fav_Down => _L.Fav_Down;
        public string Fav_Open => _L.Fav_Open;
        public string Fav_OpenTooltip => _L.Fav_OpenTooltip;
        public string Fav_CopyUrl => _L.Fav_CopyUrl;
        public string Fav_Close => _L.Fav_Close;
        public string Fav_Export => _L.Fav_Export;
        public string Fav_ExportHtml => _L.Fav_ExportHtml;
        public string Fav_ExportHtmlTooltip => _L.Fav_ExportHtmlTooltip;
        public string Fav_ExportJson => _L.Fav_ExportJson;
        public string Fav_ExportCsv => _L.Fav_ExportCsv;
        public string Fav_Import => _L.Fav_Import;
        public string Fav_ImportHtml => _L.Fav_ImportHtml;
        public string Fav_ImportHtmlTooltip => _L.Fav_ImportHtmlTooltip;
        public string Fav_ImportJson => _L.Fav_ImportJson;
        public string Fav_ImportFrom => _L.Fav_ImportFrom;
        public string Fav_ImportBtn => _L.Fav_ImportBtn;
        public string Fav_Search => _L.Fav_Search;
        public string Fav_Group => _L.Fav_Group;
        public string Fav_Clear => _L.Fav_Clear;
        public string Fav_AllGroups => _L.Fav_AllGroups;
        public string Fav_ColTitle => _L.Fav_ColTitle;
        public string Fav_ColGroup => _L.Fav_ColGroup;
        public string Fav_CtxOpen => _L.Fav_CtxOpen;
        public string Fav_CtxEdit => _L.Fav_CtxEdit;
        public string Fav_CtxCopy => _L.Fav_CtxCopy;
        public string Fav_CtxDelete => _L.Fav_CtxDelete;
        public string Fav_Msg_Duplicate => _L.Fav_Msg_Duplicate;
        public string Fav_Msg_DuplicateTitle => _L.Fav_Msg_DuplicateTitle;
        public string Fav_Msg_ConfirmDelete => _L.Fav_Msg_ConfirmDelete;
        public string Fav_Msg_Confirm => _L.Fav_Msg_Confirm;
        public string Fav_Msg_ImportResult => _L.Fav_Msg_ImportResult;
        public string Fav_Msg_ImportTitle => _L.Fav_Msg_ImportTitle;
        public string Fav_Msg_ImportJsonError => _L.Fav_Msg_ImportJsonError;
        public string Fav_Msg_ImportJsonTitle => _L.Fav_Msg_ImportJsonTitle;
        public string Fav_Msg_BrowserNotFound => _L.Fav_Msg_BrowserNotFound;
        public string Fav_Msg_NotFound => _L.Fav_Msg_NotFound;
        public string Fav_Msg_FirefoxNotFound => _L.Fav_Msg_FirefoxNotFound;
        public string Fav_Msg_FirefoxFallback => _L.Fav_Msg_FirefoxFallback;
        public string Fav_Msg_FirefoxTitle => _L.Fav_Msg_FirefoxTitle;
        public string Fav_Msg_SqliteRequired => _L.Fav_Msg_SqliteRequired;
        public string Fav_Msg_ErrorOpenUrl => _L.Fav_Msg_ErrorOpenUrl;
        public string Fav_StatusReady => _L.Fav_StatusReady;
        public string Fav_StatusAdded => _L.Fav_StatusAdded;
        public string Fav_StatusEdited => _L.Fav_StatusEdited;
        public string Fav_StatusDeleted => _L.Fav_StatusDeleted;
        public string Fav_StatusUrlCopied => _L.Fav_StatusUrlCopied;
        public string Fav_StatusExported => _L.Fav_StatusExported;
        public string Fav_StatusImported => _L.Fav_StatusImported;
        public string Fav_Count => _L.Fav_Count;
        // ── HISTORYWINDOW ──
        public string History_WindowTitle => _L.History_WindowTitle;
        public string History_Title => _L.History_Title;
        public string History_IncognitoNote => _L.History_IncognitoNote;
        public string History_DeleteAll => _L.History_DeleteAll;
        public string History_Today => _L.History_Today;
        public string History_Yesterday => _L.History_Yesterday;
        public string History_ThisWeek => _L.History_ThisWeek;
        public string History_ThisMonth => _L.History_ThisMonth;
        public string History_SearchResults => _L.History_SearchResults;
        public string History_Msg_ConfirmDelete => _L.History_Msg_ConfirmDelete;
        public string History_Msg_Confirmation => _L.History_Msg_Confirmation;
        // ── MAINWINDOW ──
        public string Main_MaxPanel1 => _L.Main_MaxPanel1;
        public string Main_MaxPanel2 => _L.Main_MaxPanel2;
        public string Main_MaxPanel3 => _L.Main_MaxPanel3;
        public string Main_MaxPanel4 => _L.Main_MaxPanel4;
        public string Main_RestorePanels => _L.Main_RestorePanels;
        public string Main_Notes => _L.Main_Notes;
        public string Main_WelcomePopup => _L.Main_WelcomePopup;
        public string Main_FavAdd => _L.Main_FavAdd;
        public string Main_FavManage => _L.Main_FavManage;
        public string Main_MenuGeneral => _L.Main_MenuGeneral;
        public string Main_MenuApariencia => _L.Main_MenuApariencia;
        public string Main_MenuFavoritos => _L.Main_MenuFavoritos;
        public string Main_MenuHistorial => _L.Main_MenuHistorial;
        public string Main_MenuPrivacidad => _L.Main_MenuPrivacidad;
        public string Main_MenuPermisos => _L.Main_MenuPermisos;
        public string Main_MenuCookies => _L.Main_MenuCookies;
        public string Main_MenuAvanzado => _L.Main_MenuAvanzado;
        public string Main_MenuAcercaDe => _L.Main_MenuAcercaDe;
        public string Main_CtxClearMark => _L.Main_CtxClearMark;
        public string Main_CtxMarkColor => _L.Main_CtxMarkColor;
        public string Main_CtxSortByColor => _L.Main_CtxSortByColor;
        public string Main_CtxGoHome => _L.Main_CtxGoHome;
        public string Main_CtxIncognitoOn => _L.Main_CtxIncognitoOn;
        public string Main_CtxIncognitoOff => _L.Main_CtxIncognitoOff;
        public string Main_CtxReload => _L.Main_CtxReload;
        public string Main_CtxOpenInWindow => _L.Main_CtxOpenInWindow;
        public string Main_CtxOpenWith => _L.Main_CtxOpenWith;
        public string Main_CtxReopenClosed => _L.Main_CtxReopenClosed;
        public string Main_CtxNewTab => _L.Main_CtxNewTab;
        public string Main_CtxNewIncognito => _L.Main_CtxNewIncognito;
        public string Main_CtxMoveTab => _L.Main_CtxMoveTab;
        public string Main_CtxCopyTab => _L.Main_CtxCopyTab;
        public string Main_CtxDuplicate => _L.Main_CtxDuplicate;
        public string Main_CtxClose => _L.Main_CtxClose;
        public string Main_CtxRestoreSession => _L.Main_CtxRestoreSession;
        public string Main_PosRight => _L.Main_PosRight;
        public string Main_PosLeft => _L.Main_PosLeft;
        public string Main_PosEnd => _L.Main_PosEnd;
        public string Main_PosStart => _L.Main_PosStart;
        public string Main_CloseThis => _L.Main_CloseThis;
        public string Main_CloseAllRight => _L.Main_CloseAllRight;
        public string Main_CloseAllLeft => _L.Main_CloseAllLeft;
        public string Main_CloseOthers => _L.Main_CloseOthers;
        public string Main_Browser1 => _L.Main_Browser1;
        public string Main_Browser2 => _L.Main_Browser2;
        public string Main_Browser3 => _L.Main_Browser3;
        public string Main_Browser4 => _L.Main_Browser4;
        public string Main_PermAllow => _L.Main_PermAllow;
        public string Main_PermBlock => _L.Main_PermBlock;
        public string Main_NotesReminderPrefix => _L.Main_NotesReminderPrefix;
        public string Main_NotesReminderSuffix => _L.Main_NotesReminderSuffix;
        public string Main_NotesReminderQuestion => _L.Main_NotesReminderQuestion;
        public string Main_NotesReminderTitle => _L.Main_NotesReminderTitle;
        public string Main_NoSession => _L.Main_NoSession;
        public string Main_RestoreSessionTitle => _L.Main_RestoreSessionTitle;
        public string Main_SecureNoInfo => _L.Main_SecureNoInfo;
        public string Main_SecureYes => _L.Main_SecureYes;
        public string Main_SecureNo => _L.Main_SecureNo;
        public string Main_ErrExternalLink => _L.Main_ErrExternalLink;
        public string Main_NoTabInit => _L.Main_NoTabInit;
        public string Main_ErrOpenBrowser => _L.Main_ErrOpenBrowser;
        public string Main_AboutTitle => _L.Main_AboutTitle;
        public string Main_AboutNotAvailable => _L.Main_AboutNotAvailable;
        public string Main_AboutVersion => _L.Main_AboutVersion;
        public string Main_AboutChromium => _L.Main_AboutChromium;
        public string Main_AboutLocalIp => _L.Main_AboutLocalIp;
        public string Main_AboutPublicIp => _L.Main_AboutPublicIp;
        public string Main_AboutAppName => _L.Main_AboutAppName;
        public string Main_PermFormat => _L.Main_PermFormat;
        public string Main_NewTab => _L.Main_NewTab;
        public string Main_Loading => _L.Main_Loading;
        // ── NOTASWINDOW ──
        public string Notas_WindowTitle => _L.Notas_WindowTitle;
        public string Notas_NewNote => _L.Notas_NewNote;
        public string Notas_Reminder => _L.Notas_Reminder;
        public string Notas_MsgDelete => _L.Notas_MsgDelete;
        public string Notas_MsgConfirm => _L.Notas_MsgConfirm;
        // ── THEMEEDITOR ──
        public string Theme_ColorDarkWebContent => _L.Theme_ColorDarkWebContent;
        public string Theme_WindowTitle => _L.Theme_WindowTitle;
        public string Theme_HeaderTitle => _L.Theme_HeaderTitle;
        public string Theme_PreviewTitle => _L.Theme_PreviewTitle;
        public string Theme_PreviewTab1 => _L.Theme_PreviewTab1;
        public string Theme_PreviewTab2 => _L.Theme_PreviewTab2;
        public string Theme_PreviewIncognito => _L.Theme_PreviewIncognito;
        public string Theme_PreviewUrl => _L.Theme_PreviewUrl;
        public string Theme_Hue => _L.Theme_Hue;
        public string Theme_Saturation => _L.Theme_Saturation;
        public string Theme_Luminosity => _L.Theme_Luminosity;
        public string Theme_SecGeneral => _L.Theme_SecGeneral;
        public string Theme_SecTabsNormal => _L.Theme_SecTabsNormal;
        public string Theme_SecNavBar => _L.Theme_SecNavBar;
        public string Theme_SecButtons => _L.Theme_SecButtons;
        public string Theme_SecButtonCentro => _L.Theme_SecButtonCentro;
        public string Theme_SecIncognito => _L.Theme_SecIncognito;
        public string Theme_ColorWindowBg => _L.Theme_ColorWindowBg;
        public string Theme_ColorWindowFg => _L.Theme_ColorWindowFg;
        public string Theme_ColorTabInactive => _L.Theme_ColorTabInactive;
        public string Theme_ColorTabActive => _L.Theme_ColorTabActive;
        public string Theme_ColorHover => _L.Theme_ColorHover;
        public string Theme_ColorTabActiveHover => _L.Theme_ColorTabActiveHover;
        public string Theme_ColorTabText => _L.Theme_ColorTabText;
        public string Theme_ColorNavBarBg => _L.Theme_ColorNavBarBg;
        public string Theme_ColorNavBarFg => _L.Theme_ColorNavBarFg;
        public string Theme_ColorUrlBg => _L.Theme_ColorUrlBg;
        public string Theme_ColorUrlFg => _L.Theme_ColorUrlFg;
        public string Theme_ColorButtonAccent => _L.Theme_ColorButtonAccent;
        public string Theme_ColorButtonPressed => _L.Theme_ColorButtonPressed;
        public string Theme_ColorButtonCentro => _L.Theme_ColorButtonCentro;
        public string Theme_ColorIncogInactive => _L.Theme_ColorIncogInactive;
        public string Theme_ColorIncogActive => _L.Theme_ColorIncogActive;
        public string Theme_ColorIncogText => _L.Theme_ColorIncogText;
        public string Theme_LabelName => _L.Theme_LabelName;
        public string Theme_BtnSave => _L.Theme_BtnSave;
        public string Theme_BtnCancel => _L.Theme_BtnCancel;
        public string Theme_PickerTitle => _L.Theme_PickerTitle;
        public string Theme_PickerHue => _L.Theme_PickerHue;
        public string Theme_PickerSaturation => _L.Theme_PickerSaturation;
        public string Theme_PickerLuminosity => _L.Theme_PickerLuminosity;
        public string Theme_PickerOk => _L.Theme_PickerOk;
        public string Theme_PickerCancel => _L.Theme_PickerCancel;
        public string Theme_MsgInitError => _L.Theme_MsgInitError;
        public string Theme_MsgInitErrorTitle => _L.Theme_MsgInitErrorTitle;
        public string Theme_MsgNoName => _L.Theme_MsgNoName;
        public string Theme_MsgNoNameTitle => _L.Theme_MsgNoNameTitle;
        public string Theme_MsgPredefined => _L.Theme_MsgPredefined;
        public string Theme_MsgPredefinedTitle => _L.Theme_MsgPredefinedTitle;
        public string Theme_MsgOverwriteFormat => _L.Theme_MsgOverwriteFormat;
        public string Theme_MsgOverwriteTitle => _L.Theme_MsgOverwriteTitle;
        public string Theme_MsgSavedFormat => _L.Theme_MsgSavedFormat;
        public string Theme_MsgSavedTitle => _L.Theme_MsgSavedTitle;
    }
}
