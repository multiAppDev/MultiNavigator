// Resources/IdiomaIt.cs
namespace Multinavigator.Idiomas
{
    internal class IdiomaIt : IIdiomaStrings
    {
        public string About_Github            => "Visualizza il codice su GitHub";
        public string About_Github_button             => "⭐ Metti una stella su GitHub";
        public string Theme_ColorDarkWebContent      => "Contenuto web in modalità oscura";
        public static readonly IdiomaIt Instance = new();
        // =============================================
        // CONFIGURACION
        // =============================================
        public string Cfg_WindowTitle                    => "Impostazioni";
        public string Cfg_Title                          => "Impostazioni";
        public string Cfg_Language_Label                 => "Lingua";
        public string Cfg_Language_Title                 => "Lingua";
        public string Cfg_Language_RestartHint           => "La lingua verrà applicata alla riapertura di ogni finestra.";

        public string Cfg_TabGeneral                     => "Generale";
        public string Cfg_TabApariencia                  => "Aspetto";
        public string Cfg_TabPrivacidad                  => "Privacy";
        public string Cfg_TabPermisos                    => "Permessi";
        public string Cfg_TabAvanzado                    => "Avanzate";

        public string Cfg_HomePages_Title                => "Pagine iniziali";
        public string Cfg_HomePages_Desc                 => "URL da aprire all'avvio di ogni pannello";
        public string Cfg_HomePages_Panel1               => "Pannello 1";
        public string Cfg_HomePages_Panel2               => "Pannello 2";
        public string Cfg_HomePages_Panel3               => "Pannello 3";
        public string Cfg_HomePages_Panel4               => "Pannello 4";
        public string Cfg_HomePages_Save                 => "Salva pagine iniziali";

        public string Cfg_Session_Title                  => "Sessione";
        public string Cfg_Session_Restore                => "Ripristina la sessione precedente all'avvio";

        public string Cfg_Search_Title                   => "Motore di ricerca predefinito";
        public string Cfg_Search_Desc                    => "Utilizzato durante la digitazione nella barra degli indirizzi";
        public string Cfg_Search_Save                    => "Salva";
        public string Cfg_Search_CustomUrl               => "URL personalizzato:";

        public string Cfg_Backup_Title                   => "Backup";
        public string Cfg_Backup_Desc                    => "Esporta o importa le tue impostazioni";
        public string Cfg_Backup_Favorites               => "Preferiti";
        public string Cfg_Backup_Themes                  => "Temi";
        public string Cfg_Backup_Permissions             => "Permessi";
        public string Cfg_Backup_Settings                => "Impostazioni";
        public string Cfg_Backup_History                 => "Cronologia";
        public string Cfg_Backup_Export                  => "Esporta impostazioni";
        public string Cfg_Backup_Import                  => "Importa impostazioni";
        public string Cfg_Backup_NoCookies               => "I cookie non sono inclusi nel backup";

        public string Cfg_Appearance_Themes_Title        => "Temi";
        public string Cfg_Appearance_Colors_Title        => "Colori personalizzati";
        public string Cfg_Appearance_OpenEditor          => "Apri editor temi";
        public string Cfg_Appearance_EditorDesc          => "Crea e modifica temi personalizzati";
        public string Cfg_Appearance_Preview             => "Anteprima";
        public string Cfg_Appearance_Tab1                => "Scheda";
        public string Cfg_Appearance_Tab2                => "Scheda attiva";
        public string Cfg_Appearance_Incognito           => "In incognito";
        public string Cfg_Appearance_Options             => "Opzioni";
        public string Cfg_Appearance_DarkIncognito       => "Modalità scura automatica nelle schede in incognito";

        public string Cfg_Privacy_Title                  => "Privacy e dati";
        public string Cfg_Privacy_Cookies                => "Cookie";
        public string Cfg_Privacy_CookiesUnit            => "KB su disco";
        public string Cfg_Privacy_CookiesManage          => "Gestisci";
        public string Cfg_Privacy_DeleteAll              => "Elimina tutti";
        public string Cfg_Privacy_History                => "Cronologia";
        public string Cfg_Privacy_HistoryUnit            => "voci";
        public string Cfg_Privacy_HistoryView            => "Visualizza cronologia";
        public string Cfg_Privacy_Cache                  => "Cache";
        public string Cfg_Privacy_CacheUnit              => "MB su disco";
        public string Cfg_Privacy_CacheDelete            => "Svuota cache";

        public string Cfg_Perms_GlobalTitle              => "Permessi globali";
        public string Cfg_Perms_DomainTitle              => "Permessi per dominio";
        public string Cfg_Perms_Camera                   => "Fotocamera";
        public string Cfg_Perms_Mic                      => "Microfono";
        public string Cfg_Perms_Location                 => "Posizione";
        public string Cfg_Perms_Notifications            => "Notifiche";
        public string Cfg_Perms_DomainCol                => "Dominio";
        public string Cfg_Perms_AddDomain                => "Aggiungi dominio";
        public string Cfg_Perms_Reset                    => "Reimposta permessi";

        public string Cfg_Adv_Title                      => "Opzioni avanzate WebView2";
        public string Cfg_Adv_Warning                    => "⚠ È necessario riavviare l'applicazione per applicare le modifiche";
        public string Cfg_Adv_BgNetworking               => "Disabilita rete in background";
        public string Cfg_Adv_BgNetworking_Desc          => "Blocca le connessioni di rete quando il browser è in background";
        public string Cfg_Adv_Sync                       => "Disabilita sincronizzazione";
        public string Cfg_Adv_Sync_Desc                  => "Impedisci la sincronizzazione dei dati con i server Google";
        public string Cfg_Adv_Translate                  => "Disabilita traduttore";
        public string Cfg_Adv_Translate_Desc             => "Rimuovi il servizio di traduzione automatica delle pagine";
        public string Cfg_Adv_Extensions                 => "Disabilita estensioni";
        public string Cfg_Adv_Extensions_Desc            => "Impedisci il caricamento delle estensioni del browser";
        public string Cfg_Adv_DefaultApps                => "Disabilita app predefinite";
        public string Cfg_Adv_DefaultApps_Desc           => "Impedisci l'installazione delle app predefinite di Chrome";
        public string Cfg_Adv_DefaultBrowserCheck        => "Salta verifica browser predefinito";
        public string Cfg_Adv_DefaultBrowserCheck_Desc   => "Non verifica né richiede di essere il browser predefinito";
        public string Cfg_Adv_Metrics                    => "Solo registrazione metriche";
        public string Cfg_Adv_Metrics_Desc               => "Le metriche vengono registrate localmente ma non inviate";
        public string Cfg_Adv_Breakpad                   => "Disabilita segnalazione errori";
        public string Cfg_Adv_Breakpad_Desc              => "Disabilita l'invio automatico di report di crash";
        public string Cfg_Adv_Phishing                   => "Disabilita rilevamento phishing";
        public string Cfg_Adv_Phishing_Desc              => "Disabilita il rilevamento lato client dei siti di phishing";
        public string Cfg_Adv_HangMonitor                => "Disabilita monitor blocchi";
        public string Cfg_Adv_HangMonitor_Desc           => "Non rileva né segnala il blocco del browser";
        public string Cfg_Adv_Repost                     => "Salta avviso reinvio modulo";
        public string Cfg_Adv_Repost_Desc                => "Non mostrare la finestra di conferma durante l'aggiornamento POST";
        public string Cfg_Adv_DomainReliability          => "Disabilita telemetria dominio";
        public string Cfg_Adv_DomainReliability_Desc     => "Impedisci l'invio di dati di affidabilità del dominio a Google";
        public string Cfg_Adv_ComponentUpdate            => "Disabilita aggiornamenti componenti";
        public string Cfg_Adv_ComponentUpdate_Desc       => "Impedisci il download automatico degli aggiornamenti dei componenti";
        public string Cfg_Adv_BgTimer                    => "Disabilita throttling timer";
        public string Cfg_Adv_BgTimer_Desc               => "I timer JS non rallentano nelle schede in background";
        public string Cfg_Adv_RendererBg                 => "Disabilita bassa priorità in background";
        public string Cfg_Adv_RendererBg_Desc            => "Mantieni la priorità del renderer anche quando la scheda è nascosta";
        public string Cfg_Adv_IpcFlood                   => "Disabilita protezione flood IPC";
        public string Cfg_Adv_IpcFlood_Desc              => "Rimuovi il limite di messaggi IPC tra i processi del browser";
        public string Cfg_Adv_StateOn                    => "✔ Attivo";
        public string Cfg_Adv_StateOff                   => "✖ Disattivo";

        public string Cfg_Msg_ConfirmDeleteTheme         => "Eliminare il tema '{0}'?";
        public string Cfg_Msg_ConfirmDeleteThemeTitle    => "Conferma eliminazione";
        public string Cfg_Msg_NoTabInitialized           => "Nessuna scheda ancora inizializzata.";
        public string Cfg_Msg_ConfirmDeleteHistory       => "Eliminare tutta la cronologia?";
        public string Cfg_Msg_ConfirmDeleteCookies       => "Eliminare tutti i cookie?";
        public string Cfg_Msg_Confirmation               => "Conferma";
        public string Cfg_Msg_CookiesDeleted             => "Cookie eliminati.";
        public string Cfg_Msg_NoActiveTabCache           => "Nessuna scheda attiva per svuotare la cache.";
        public string Cfg_Msg_CacheDeleted               => "Cache svuotata.";
        public string Cfg_Msg_HomePagesSaved             => "Pagine iniziali salvate.";
        public string Cfg_Msg_SearchSaved                => "Motore di ricerca salvato.";
        public string Cfg_Msg_ExportOk                   => "Impostazioni esportate con successo.";
        public string Cfg_Msg_ImportOk                   => "Impostazioni importate con successo.\nRiavvia l'app per applicare tutte le modifiche.";
        public string Cfg_Msg_ExportError                => "Errore esportazione: {0}";
        public string Cfg_Msg_ImportError                => "Errore importazione: {0}";
        public string Cfg_Msg_Export                     => "Esporta";
        public string Cfg_Msg_Import                     => "Importa";
        public string Cfg_Msg_Saved                      => "Salvato";
        public string Cfg_Msg_Done                       => "Fatto";
        public string Cfg_Msg_Error                      => "Errore";
        public string Cfg_Msg_Notice                     => "Avviso";

        // =============================================
        // COOKIEMANAGERWINDOW
        // =============================================
        public string Cookie_WindowTitle     => "Gestione Cookie";
        public string Cookie_SearchDomain    => "Cerca dominio:";
        public string Cookie_Reload          => "Aggiorna";
        public string Cookie_Domains         => "Domini";
        public string Cookie_Cookies         => "Cookie";
        public string Cookie_SearchCookie    => "Cerca cookie:";
        public string Cookie_ColName         => "Nome";
        public string Cookie_ColValue        => "Valore";
        public string Cookie_ColPath         => "Percorso";
        public string Cookie_ColExpires      => "Scadenza";
        public string Cookie_Details         => "Dettagli";
        public string Cookie_DetailName      => "Nome:";
        public string Cookie_DetailValue     => "Valore:";
        public string Cookie_DetailDomain    => "Dominio:";
        public string Cookie_DetailPath      => "Percorso:";
        public string Cookie_DetailExpires   => "Scadenza:";
        public string Cookie_DetailFlags     => "Flag:";
        public string Cookie_FlagsFormat     => "Secure={0}, HttpOnly={1}, Session={2}";
        public string Cookie_CopyValue       => "Copia valore";
        public string Cookie_EditCookie      => "Modifica cookie";
        public string Cookie_DeleteCookie    => "Elimina cookie";
        public string Cookie_DeleteDomain    => "Elimina cookie del dominio";
        public string Cookie_DeleteAll       => "Elimina TUTTI i cookie";
        public string Cookie_Close           => "Chiudi";

        // =============================================
        // EDITCOOKIEWINDOW
        // =============================================
        public string EditCookie_WindowTitle     => "Modifica Cookie";
        public string EditCookie_Name            => "Nome:";
        public string EditCookie_Value           => "Valore:";
        public string EditCookie_Path            => "Percorso:";
        public string EditCookie_Expires         => "Data di scadenza:";
        public string EditCookie_Save            => "Salva";
        public string EditCookie_Cancel          => "Annulla";
        public string EditCookie_Msg_EmptyValue  => "Il valore del cookie non può essere vuoto.";
        public string EditCookie_Msg_EmptyPath   => "Il percorso non può essere vuoto.";
        public string EditCookie_Msg_PathSlash   => "Il percorso deve iniziare con '/'.";
        public string EditCookie_Msg_NoDate      => "Devi selezionare una data di scadenza.";
        public string EditCookie_Msg_PastDate    => "La data di scadenza non può essere nel passato.";
        public string EditCookie_Msg_SaveError   => "Errore salvataggio cookie: {0}";

        // =============================================
        // FAVORITEEDITDIALOG
        // =============================================
        public string FavDlg_WindowTitle       => "Segnalibro";
        public string FavDlg_Title             => "Titolo:";
        public string FavDlg_Url               => "URL:";
        public string FavDlg_Folder            => "Gruppo/Cartella:";
        public string FavDlg_Favicon           => "URL Favicon:";
        public string FavDlg_FaviconTooltip    => "Opzionale — lascia vuoto per il rilevamento automatico";
        public string FavDlg_Save              => "Salva";
        public string FavDlg_Cancel            => "Annulla";
        public string FavDlg_Msg_UrlRequired   => "L'URL è obbligatorio.";
        public string FavDlg_Msg_Validation    => "Validazione";

        // =============================================
        // FAVORITESWINDOW
        // =============================================
        public string Fav_WindowTitle        => "Gestione Preferiti";
        public string Fav_Add                => "➕ Aggiungi";
        public string Fav_Edit               => "✏ Modifica";
        public string Fav_Delete             => "🗑 Elimina";
        public string Fav_Up                 => "▲ Su";
        public string Fav_Down               => "▼ Giù";
        public string Fav_Open               => "🌐 Apri";
        public string Fav_OpenTooltip        => "Apri nel browser (o doppio clic sulla riga)";
        public string Fav_CopyUrl            => "📋 Copia URL";
        public string Fav_Close              => "✖ Chiudi";
        public string Fav_Export             => "Esporta:";
        public string Fav_ExportHtml         => "📄 HTML";
        public string Fav_ExportHtmlTooltip  => "Netscape Bookmark HTML — importabile in Chrome, Edge, Firefox";
        public string Fav_ExportJson         => "💾 JSON";
        public string Fav_ExportCsv          => "📊 CSV";
        public string Fav_Import             => "Importa:";
        public string Fav_ImportHtml         => "📂 HTML";
        public string Fav_ImportHtmlTooltip  => "Netscape Bookmark HTML (esportato da qualsiasi browser)";
        public string Fav_ImportJson         => "📂 JSON";
        public string Fav_ImportFrom         => "Importa segnalibri da:";
        public string Fav_ImportBtn          => "Importa";
        public string Fav_Search             => "🔍 Cerca:";
        public string Fav_Group              => "  Gruppo:";
        public string Fav_Clear              => "Cancella";
        public string Fav_AllGroups          => "(Tutti)";
        public string Fav_ColTitle           => "Titolo";
        public string Fav_ColGroup           => "Gruppo";
        public string Fav_CtxOpen            => "🌐 Apri nel browser";
        public string Fav_CtxEdit            => "✏ Modifica";
        public string Fav_CtxCopy            => "📋 Copia URL";
        public string Fav_CtxDelete          => "🗑 Elimina";
        public string Fav_Msg_Duplicate      => "Esiste già un segnalibro con questo URL.";
        public string Fav_Msg_DuplicateTitle => "Duplicato";
        public string Fav_Msg_ConfirmDelete  => "Eliminare {0} segnalibri?";
        public string Fav_Msg_Confirm        => "Conferma";
        public string Fav_Msg_ImportResult   => "Importazione completata:\n\n  ✔ Aggiunti:  {0}\n  ⏭ Saltati: {1} (duplicati)";
        public string Fav_Msg_ImportTitle    => "Importa";
        public string Fav_Msg_ImportJsonError => "Errore: {0}";
        public string Fav_Msg_ImportJsonTitle => "Importa JSON";
        public string Fav_Msg_BrowserNotFound => "Nessun segnalibro trovato per {0}.\n{1}";
        public string Fav_Msg_NotFound       => "Non trovato";
        public string Fav_Msg_FirefoxNotFound => "Cartella profilo Firefox non trovata.";
        public string Fav_Msg_FirefoxFallback => "Impossibile leggere places.sqlite di Firefox (Firefox potrebbe essere in esecuzione).\n\nAlternativa: in Firefox, vai su\n  Segnalibri ▸ Gestisci segnalibri ▸ Importa ed esegui backup ▸ Esporta segnalibri in HTML\ne usa 'Importa HTML' qui.\n\nAprire l'importazione HTML ora?";
        public string Fav_Msg_FirefoxTitle   => "Firefox";
        public string Fav_Msg_SqliteRequired => "Installa il pacchetto NuGet Microsoft.Data.Sqlite per leggere i database Firefox.";
        public string Fav_Msg_ErrorOpenUrl   => "Errore apertura URL";
        public string Fav_StatusReady        => "Pronto";
        public string Fav_StatusAdded        => "Aggiunto: {0}";
        public string Fav_StatusEdited       => "Modificato: {0}";
        public string Fav_StatusDeleted      => "Eliminato: {0}";
        public string Fav_StatusUrlCopied    => "URL copiato negli appunti";
        public string Fav_StatusExported     => "Esportati {0} → {1}";
        public string Fav_StatusImported     => "{0} segnalibri importati — {1} duplicati saltati";
        public string Fav_Count              => "{0} / {1} segnalibri";

        // =============================================
        // HISTORYWINDOW
        // =============================================
        public string History_WindowTitle        => "Cronologia di navigazione";
        public string History_Title              => "🕐 Cronologia";
        public string History_IncognitoNote      => "I siti visitati in modalità in incognito non appaiono qui.";
        public string History_DeleteAll          => "🗑 Elimina tutto";
        public string History_Today              => "Oggi";
        public string History_Yesterday          => "Ieri";
        public string History_ThisWeek           => "Questa settimana";
        public string History_ThisMonth          => "Questo mese";
        public string History_SearchResults      => "Risultati per \"{0}\"";
        public string History_Msg_ConfirmDelete  => "Eliminare tutta la cronologia?";
        public string History_Msg_Confirmation   => "Conferma";

        // =============================================
        // MAINWINDOW
        // =============================================
        public string Main_MaxPanel1            => "Ingrandisci pannello 1";
        public string Main_MaxPanel2            => "Ingrandisci pannello 2";
        public string Main_MaxPanel3            => "Ingrandisci pannello 3";
        public string Main_MaxPanel4            => "Ingrandisci pannello 4";
        public string Main_RestorePanels        => "Ripristina tutti i pannelli";
        public string Main_Notes                => "Note";
        public string Main_WelcomePopup         => "⚙ Imposta le pagine iniziali e altro...";
        public string Main_FavAdd               => "Aggiungi ai preferiti";
        public string Main_FavManage            => "📁 Gestisci preferiti";
        public string Main_MenuGeneral          => "Generale";
        public string Main_MenuApariencia       => "Aspetto";
        public string Main_MenuFavoritos        => "Preferiti";
        public string Main_MenuHistorial        => "Cronologia";
        public string Main_MenuPrivacidad       => "Privacy";
        public string Main_MenuPermisos         => "Permessi";
        public string Main_MenuCookies          => "Cookie";
        public string Main_MenuAvanzado         => "Avanzate";
        public string Main_MenuAcercaDe         => "Informazioni...";
        public string Main_CtxClearMark         => "Nessuna etichetta";
        public string Main_CtxMarkColor         => "Etichetta con colore";
        public string Main_CtxSortByColor       => "Ordina per colore";
        public string Main_CtxGoHome            => "Vai alla pagina iniziale";
        public string Main_CtxIncognitoOn       => "Attiva modalità in incognito";
        public string Main_CtxIncognitoOff      => "Disattiva modalità in incognito";
        public string Main_CtxReload            => "Ricarica pagina";
        public string Main_CtxOpenInWindow      => "Apri in nuova finestra";
        public string Main_CtxOpenWith          => "Apri con...";
        public string Main_CtxReopenClosed      => "Riapri scheda chiusa";
        public string Main_CtxNewTab            => "Nuova scheda";
        public string Main_CtxNewIncognito      => "Nuova scheda in incognito";
        public string Main_CtxMoveTab           => "Sposta scheda";
        public string Main_CtxCopyTab           => "Copia scheda";
        public string Main_CtxDuplicate         => "Duplica scheda (a destra)";
        public string Main_CtxClose             => "Chiudi schede";
        public string Main_CtxRestoreSession    => "Ripristina schede dell'ultima sessione";
        public string Main_PosRight             => "A destra";
        public string Main_PosLeft              => "A sinistra";
        public string Main_PosEnd               => "In fondo";
        public string Main_PosStart             => "All'inizio";
        public string Main_CloseThis            => "Questa scheda";
        public string Main_CloseAllRight        => "Tutte a destra";
        public string Main_CloseAllLeft         => "Tutte a sinistra";
        public string Main_CloseOthers          => "Tutte tranne questa";
        public string Main_Browser1             => "Browser 1";
        public string Main_Browser2             => "Browser 2";
        public string Main_Browser3             => "Browser 3";
        public string Main_Browser4             => "Browser 4";
        public string Main_PermAllow            => "Consenti";
        public string Main_PermBlock            => "Blocca";
        public string Main_NotesReminderPrefix  => "Hai ";
        public string Main_NotesReminderSuffix  => " promemoria di note in sospeso:\n\n";
        public string Main_NotesReminderQuestion => "\nVuoi vedere le note?";
        public string Main_NotesReminderTitle   => "Promemoria note";
        public string Main_NoSession            => "Nessuna sessione salvata.";
        public string Main_RestoreSessionTitle  => "Ripristina sessione";
        public string Main_SecureNoInfo         => "Informazioni di sicurezza non disponibili.";
        public string Main_SecureYes            => "Connessione sicura.\nIl sito usa HTTPS.";
        public string Main_SecureNo             => "Connessione NON SICURA.\nIl sito non usa HTTPS.";
        public string Main_ErrExternalLink      => "Errore apertura link esterno: ";
        public string Main_NoTabInit            => "Nessuna scheda ancora inizializzata.";
        public string Main_ErrOpenBrowser       => "Impossibile aprire il browser: ";
        public string Main_AboutTitle           => "Informazioni su Multinavigator";
        public string Main_AboutNotAvailable    => "Non disponibile";
        public string Main_AboutVersion         => "Versione: 7.0.0";
        public string Main_AboutChromium        => "Versione Chromium: ";
        public string Main_AboutLocalIp         => "🏠  IP locale: ";
        public string Main_AboutPublicIp        => "🌍  IP pubblico: ";
        public string Main_AboutAppName         => "Multinavigator 7";
        public string Main_PermFormat           => "{0} vuole usare: {1}";
        public string Main_NewTab               => "Nuova scheda";
        public string Main_Loading              => "Caricamento...";

        // =============================================
        // NOTASWINDOW
        // =============================================
        public string Notas_WindowTitle         => "Note";
        public string Notas_NewNote             => "+ Nuova nota";
        public string Notas_Reminder            => "Promemoria";
        public string Notas_MsgDelete           => "Eliminare questa nota?";
        public string Notas_MsgConfirm          => "Conferma";

        // =============================================
        // THEMEEDITOR
        // =============================================
        public string Theme_WindowTitle         => "Editor Temi Personalizzati";
        public string Theme_HeaderTitle         => "🎨 Editor Temi";
        public string Theme_PreviewTitle        => "Anteprima";
        public string Theme_PreviewTab1         => "Scheda 1";
        public string Theme_PreviewTab2         => "Scheda 2";
        public string Theme_PreviewIncognito    => "🕵️ Incognito";
        public string Theme_PreviewUrl          => "🔒 https://esempio.com";
        public string Theme_Hue                 => "🎨 Tonalità";
        public string Theme_Saturation          => "💧 Saturazione";
        public string Theme_Luminosity          => "💡 Luminosità";
        public string Theme_SecGeneral          => "🎨 Colori Generali";
        public string Theme_SecTabsNormal       => "📑 Schede Normali";
        public string Theme_SecNavBar           => "🧭 Barra di Navigazione";
        public string Theme_SecButtons          => "🔘 Pulsanti";
        public string Theme_SecButtonCentro     => "🔘 Pulsante Centrale";
        public string Theme_SecIncognito        => "🕵️ Schede In Incognito";
        public string Theme_ColorWindowBg       => "Sfondo finestra";
        public string Theme_ColorWindowFg       => "Testo finestra";
        public string Theme_ColorTabInactive    => "Scheda inattiva";
        public string Theme_ColorTabActive      => "Scheda attiva";
        public string Theme_ColorHover          => "Hover";
        public string Theme_ColorTabActiveHover => "Attiva + Hover";
        public string Theme_ColorTabText        => "Testo scheda";
        public string Theme_ColorNavBarBg       => "Sfondo barra";
        public string Theme_ColorNavBarFg       => "Testo";
        public string Theme_ColorUrlBg          => "Sfondo URL";
        public string Theme_ColorUrlFg          => "Testo URL";
        public string Theme_ColorButtonAccent   => "Colore pulsante";
        public string Theme_ColorButtonPressed  => "Premuto";
        public string Theme_ColorButtonCentro   => "Colore pulsante centrale";
        public string Theme_ColorIncogInactive  => "Inattivo";
        public string Theme_ColorIncogActive    => "Attivo";
        public string Theme_ColorIncogText      => "Testo";
        public string Theme_LabelName           => "Nome:";
        public string Theme_BtnSave             => "💾 Salva";
        public string Theme_BtnCancel           => "❌ Annulla";
        public string Theme_PickerTitle         => "Selettore colore";
        public string Theme_PickerHue           => "Tonalità (H)";
        public string Theme_PickerSaturation    => "Saturazione (S)";
        public string Theme_PickerLuminosity    => "Luminosità (L)";
        public string Theme_PickerOk            => "OK";
        public string Theme_PickerCancel        => "Annulla";
        public string Theme_MsgInitError        => "Errore inizializzazione editor: ";
        public string Theme_MsgInitErrorTitle   => "Errore";
        public string Theme_MsgNoName           => "Inserisci un nome per il tema.";
        public string Theme_MsgNoNameTitle      => "Nome richiesto";
        public string Theme_MsgPredefined       => "è un tema predefinito e non può essere sovrascritto.\nScegli un nome diverso.";
        public string Theme_MsgPredefinedTitle  => "Nome riservato";
        public string Theme_MsgOverwriteFormat  => "Esiste già un tema personalizzato chiamato '{0}'.\nSovrascrivere?";
        public string Theme_MsgOverwriteTitle   => "Tema esistente";
        public string Theme_MsgSavedFormat      => "Tema '{0}' salvato con successo.";
        public string Theme_MsgSavedTitle       => "Tema salvato";
    }
}
