// Resources/IdiomaEs.cs
namespace Multinavigator.Idiomas
{
    internal class IdiomaEs : IIdiomaStrings
    {
        public string About_Github            => "Ver código en GitHub";
        public string About_Github_button             => "⭐ Dale una estrella en GitHub";
        public string Theme_ColorDarkWebContent      => "Contenido web en modo oscuro";
        public static readonly IdiomaEs Instance = new();
        // =============================================
        // CONFIGURACION
        // =============================================
        public string Cfg_WindowTitle                    => "Configuración";
        public string Cfg_Title                          => "Configuración";
        public string Cfg_Language_Label                 => "Idioma";
        public string Cfg_Language_Title                 => "Idioma";
        public string Cfg_Language_RestartHint           => "El idioma se aplicará al reabrir cada ventana.";

        public string Cfg_TabGeneral                     => "General";
        public string Cfg_TabApariencia                  => "Apariencia";
        public string Cfg_TabPrivacidad                  => "Privacidad";
        public string Cfg_TabPermisos                    => "Permisos";
        public string Cfg_TabAvanzado                    => "Avanzado";

        public string Cfg_HomePages_Title                => "Páginas de inicio";
        public string Cfg_HomePages_Desc                 => "URL que se abrirá al iniciar cada panel";
        public string Cfg_HomePages_Panel1               => "Panel 1";
        public string Cfg_HomePages_Panel2               => "Panel 2";
        public string Cfg_HomePages_Panel3               => "Panel 3";
        public string Cfg_HomePages_Panel4               => "Panel 4";
        public string Cfg_HomePages_Save                 => "Guardar páginas de inicio";

        public string Cfg_Session_Title                  => "Sesión";
        public string Cfg_Session_Restore                => "Restaurar la sesión anterior al iniciar";

        public string Cfg_Search_Title                   => "Buscador por defecto";
        public string Cfg_Search_Desc                    => "Se usa al escribir en la barra de dirección";
        public string Cfg_Search_Save                    => "Guardar";
        public string Cfg_Search_CustomUrl               => "URL personalizada:";

        public string Cfg_Backup_Title                   => "Copia de seguridad";
        public string Cfg_Backup_Desc                    => "Exporta o importa tu configuración";
        public string Cfg_Backup_Favorites               => "Favoritos";
        public string Cfg_Backup_Themes                  => "Temas";
        public string Cfg_Backup_Permissions             => "Permisos";
        public string Cfg_Backup_Settings                => "Ajustes";
        public string Cfg_Backup_History                 => "Historial";
        public string Cfg_Backup_Export                  => "Exportar configuración";
        public string Cfg_Backup_Import                  => "Importar configuración";
        public string Cfg_Backup_NoCookies               => "Las cookies no se incluyen en la copia de seguridad";

        public string Cfg_Appearance_Themes_Title        => "Temas";
        public string Cfg_Appearance_Colors_Title        => "Colores personalizados";
        public string Cfg_Appearance_OpenEditor          => "Abrir editor de temas";
        public string Cfg_Appearance_EditorDesc          => "Crea y edita temas personalizados";
        public string Cfg_Appearance_Preview             => "Vista previa";
        public string Cfg_Appearance_Tab1                => "Pestaña";
        public string Cfg_Appearance_Tab2                => "Pestaña activa";
        public string Cfg_Appearance_Incognito           => "Incógnito";
        public string Cfg_Appearance_Options             => "Opciones";
        public string Cfg_Appearance_DarkIncognito       => "Modo oscuro automático en pestañas incógnito";

        public string Cfg_Privacy_Title                  => "Privacidad y datos";
        public string Cfg_Privacy_Cookies                => "Cookies";
        public string Cfg_Privacy_CookiesUnit            => "KB en disco";
        public string Cfg_Privacy_CookiesManage          => "Gestionar";
        public string Cfg_Privacy_DeleteAll              => "Eliminar todo";
        public string Cfg_Privacy_History                => "Historial";
        public string Cfg_Privacy_HistoryUnit            => "entradas";
        public string Cfg_Privacy_HistoryView            => "Ver historial";
        public string Cfg_Privacy_Cache                  => "Caché";
        public string Cfg_Privacy_CacheUnit              => "MB en disco";
        public string Cfg_Privacy_CacheDelete            => "Eliminar caché";

        public string Cfg_Perms_GlobalTitle              => "Permisos globales";
        public string Cfg_Perms_DomainTitle              => "Permisos por dominio";
        public string Cfg_Perms_Camera                   => "Cámara";
        public string Cfg_Perms_Mic                      => "Micrófono";
        public string Cfg_Perms_Location                 => "Ubicación";
        public string Cfg_Perms_Notifications            => "Notificaciones";
        public string Cfg_Perms_DomainCol                => "Dominio";
        public string Cfg_Perms_AddDomain                => "Añadir dominio";
        public string Cfg_Perms_Reset                    => "Restablecer permisos";

        public string Cfg_Adv_Title                      => "Opciones avanzadas de WebView2";
        public string Cfg_Adv_Warning                    => "⚠ Requiere reiniciar la aplicación para aplicarse";
        public string Cfg_Adv_BgNetworking               => "Deshabilitar red en segundo plano";
        public string Cfg_Adv_BgNetworking_Desc          => "Bloquea conexiones de red cuando el navegador está en segundo plano";
        public string Cfg_Adv_Sync                       => "Deshabilitar sincronización";
        public string Cfg_Adv_Sync_Desc                  => "Impide la sincronización de datos con servidores de Google";
        public string Cfg_Adv_Translate                  => "Deshabilitar traductor";
        public string Cfg_Adv_Translate_Desc             => "Elimina el servicio de traducción automática de páginas";
        public string Cfg_Adv_Extensions                 => "Deshabilitar extensiones";
        public string Cfg_Adv_Extensions_Desc            => "Impide la carga de extensiones del navegador";
        public string Cfg_Adv_DefaultApps                => "Deshabilitar apps por defecto";
        public string Cfg_Adv_DefaultApps_Desc           => "Evita la instalación de aplicaciones predeterminadas de Chrome";
        public string Cfg_Adv_DefaultBrowserCheck        => "Omitir comprobación de navegador predeterminado";
        public string Cfg_Adv_DefaultBrowserCheck_Desc   => "No comprueba ni solicita ser el navegador predeterminado";
        public string Cfg_Adv_Metrics                    => "Solo registro de métricas";
        public string Cfg_Adv_Metrics_Desc               => "Las métricas se registran localmente pero no se envían";
        public string Cfg_Adv_Breakpad                   => "Deshabilitar informes de error";
        public string Cfg_Adv_Breakpad_Desc              => "Desactiva el envío automático de informes de fallos";
        public string Cfg_Adv_Phishing                   => "Deshabilitar detección de phishing";
        public string Cfg_Adv_Phishing_Desc              => "Desactiva la detección de sitios de phishing en el cliente";
        public string Cfg_Adv_HangMonitor                => "Deshabilitar monitor de bloqueos";
        public string Cfg_Adv_HangMonitor_Desc           => "No detecta ni reporta cuando el navegador se congela";
        public string Cfg_Adv_Repost                     => "Omitir aviso al reenviar formulario";
        public string Cfg_Adv_Repost_Desc                => "No muestra el diálogo de confirmación al recargar un POST";
        public string Cfg_Adv_DomainReliability          => "Deshabilitar telemetría de dominio";
        public string Cfg_Adv_DomainReliability_Desc     => "Evita el envío de datos de fiabilidad de dominios a Google";
        public string Cfg_Adv_ComponentUpdate            => "Deshabilitar actualización de componentes";
        public string Cfg_Adv_ComponentUpdate_Desc       => "Impide la descarga automática de actualizaciones de componentes";
        public string Cfg_Adv_BgTimer                    => "Deshabilitar throttling de temporizadores";
        public string Cfg_Adv_BgTimer_Desc               => "Los temporizadores JS no se ralentizan en pestañas en segundo plano";
        public string Cfg_Adv_RendererBg                 => "Deshabilitar prioridad baja en segundo plano";
        public string Cfg_Adv_RendererBg_Desc            => "El renderer mantiene su prioridad aunque la pestaña esté oculta";
        public string Cfg_Adv_IpcFlood                   => "Deshabilitar protección anti-flood IPC";
        public string Cfg_Adv_IpcFlood_Desc              => "Elimina el límite de mensajes IPC entre procesos del navegador";
        public string Cfg_Adv_StateOn                    => "✔ Activado";
        public string Cfg_Adv_StateOff                   => "✖ Desactivado";

        public string Cfg_Msg_ConfirmDeleteTheme         => "¿Eliminar el tema '{0}'?";
        public string Cfg_Msg_ConfirmDeleteThemeTitle    => "Confirmar eliminación";
        public string Cfg_Msg_NoTabInitialized           => "No hay ninguna pestaña inicializada todavía.";
        public string Cfg_Msg_ConfirmDeleteHistory       => "¿Eliminar todo el historial?";
        public string Cfg_Msg_ConfirmDeleteCookies       => "¿Eliminar todas las cookies?";
        public string Cfg_Msg_Confirmation               => "Confirmación";
        public string Cfg_Msg_CookiesDeleted             => "Cookies eliminadas.";
        public string Cfg_Msg_NoActiveTabCache           => "No hay ninguna pestaña activa para limpiar la caché.";
        public string Cfg_Msg_CacheDeleted               => "Caché eliminada.";
        public string Cfg_Msg_HomePagesSaved             => "Páginas de inicio guardadas.";
        public string Cfg_Msg_SearchSaved                => "Buscador guardado.";
        public string Cfg_Msg_ExportOk                   => "Configuración exportada correctamente.";
        public string Cfg_Msg_ImportOk                   => "Configuración importada correctamente.\nReinicia la aplicación para aplicar todos los cambios.";
        public string Cfg_Msg_ExportError                => "Error al exportar: {0}";
        public string Cfg_Msg_ImportError                => "Error al importar: {0}";
        public string Cfg_Msg_Export                     => "Exportar";
        public string Cfg_Msg_Import                     => "Importar";
        public string Cfg_Msg_Saved                      => "Guardado";
        public string Cfg_Msg_Done                       => "Listo";
        public string Cfg_Msg_Error                      => "Error";
        public string Cfg_Msg_Notice                     => "Aviso";

        // =============================================
        // COOKIEMANAGERWINDOW
        // =============================================
        public string Cookie_WindowTitle     => "Gestor de cookies";
        public string Cookie_SearchDomain    => "Buscar dominio:";
        public string Cookie_Reload          => "Recargar";
        public string Cookie_Domains         => "Dominios";
        public string Cookie_Cookies         => "Cookies";
        public string Cookie_SearchCookie    => "Buscar cookie:";
        public string Cookie_ColName         => "Nombre";
        public string Cookie_ColValue        => "Valor";
        public string Cookie_ColPath         => "Path";
        public string Cookie_ColExpires      => "Expira";
        public string Cookie_Details         => "Detalles";
        public string Cookie_DetailName      => "Nombre:";
        public string Cookie_DetailValue     => "Valor:";
        public string Cookie_DetailDomain    => "Dominio:";
        public string Cookie_DetailPath      => "Path:";
        public string Cookie_DetailExpires   => "Expira:";
        public string Cookie_DetailFlags     => "Flags:";
        public string Cookie_FlagsFormat     => "Secure={0}, HttpOnly={1}, Session={2}";
        public string Cookie_CopyValue       => "Copiar valor";
        public string Cookie_EditCookie      => "Editar cookie";
        public string Cookie_DeleteCookie    => "Eliminar cookie";
        public string Cookie_DeleteDomain    => "Eliminar cookies del dominio";
        public string Cookie_DeleteAll       => "Eliminar TODAS las cookies";
        public string Cookie_Close           => "Cerrar";

        // =============================================
        // EDITCOOKIEWINDOW
        // =============================================
        public string EditCookie_WindowTitle     => "Editar cookie";
        public string EditCookie_Name            => "Nombre:";
        public string EditCookie_Value           => "Valor:";
        public string EditCookie_Path            => "Path:";
        public string EditCookie_Expires         => "Expiración:";
        public string EditCookie_Save            => "Guardar";
        public string EditCookie_Cancel          => "Cancelar";
        public string EditCookie_Msg_EmptyValue  => "El valor de la cookie no puede estar vacío.";
        public string EditCookie_Msg_EmptyPath   => "El Path no puede estar vacío.";
        public string EditCookie_Msg_PathSlash   => "El Path debe comenzar con '/'.";
        public string EditCookie_Msg_NoDate      => "Debes seleccionar una fecha de expiración.";
        public string EditCookie_Msg_PastDate    => "La fecha de expiración no puede estar en el pasado.";
        public string EditCookie_Msg_SaveError   => "Error al guardar la cookie: {0}";

        // =============================================
        // FAVORITEEDITDIALOG
        // =============================================
        public string FavDlg_WindowTitle       => "Marcador";
        public string FavDlg_Title             => "Título:";
        public string FavDlg_Url               => "URL:";
        public string FavDlg_Folder            => "Grupo/Carpeta:";
        public string FavDlg_Favicon           => "URL del favicon:";
        public string FavDlg_FaviconTooltip    => "Opcional — déjalo vacío para detectarlo automáticamente";
        public string FavDlg_Save              => "Guardar";
        public string FavDlg_Cancel            => "Cancelar";
        public string FavDlg_Msg_UrlRequired   => "La URL es obligatoria.";
        public string FavDlg_Msg_Validation    => "Validación";

        // =============================================
        // FAVORITESWINDOW
        // =============================================
        public string Fav_WindowTitle        => "Gestor de favoritos";
        public string Fav_Add                => "➕ Añadir";
        public string Fav_Edit               => "✏ Editar";
        public string Fav_Delete             => "🗑 Eliminar";
        public string Fav_Up                 => "▲ Subir";
        public string Fav_Down               => "▼ Bajar";
        public string Fav_Open               => "🌐 Abrir";
        public string Fav_OpenTooltip        => "Abrir en el navegador (o doble clic en la fila)";
        public string Fav_CopyUrl            => "📋 Copiar URL";
        public string Fav_Close              => "✖ Cerrar";
        public string Fav_Export             => "Exportar:";
        public string Fav_ExportHtml         => "📄 HTML";
        public string Fav_ExportHtmlTooltip  => "Netscape Bookmark HTML — importable en Chrome, Edge, Firefox";
        public string Fav_ExportJson         => "💾 JSON";
        public string Fav_ExportCsv          => "📊 CSV";
        public string Fav_Import             => "Importar:";
        public string Fav_ImportHtml         => "📂 HTML";
        public string Fav_ImportHtmlTooltip  => "Netscape Bookmark HTML (exportado desde cualquier navegador)";
        public string Fav_ImportJson         => "📂 JSON";
        public string Fav_ImportFrom         => "Importar favoritos de:";
        public string Fav_ImportBtn          => "Importar";
        public string Fav_Search             => "🔍 Buscar:";
        public string Fav_Group              => "  Grupo:";
        public string Fav_Clear              => "Limpiar";
        public string Fav_AllGroups          => "(Todos)";
        public string Fav_ColTitle           => "Título";
        public string Fav_ColGroup           => "Grupo";
        public string Fav_CtxOpen            => "🌐 Abrir en navegador";
        public string Fav_CtxEdit            => "✏ Editar";
        public string Fav_CtxCopy            => "📋 Copiar URL";
        public string Fav_CtxDelete          => "🗑 Eliminar";
        public string Fav_Msg_Duplicate      => "Ya existe un marcador con esa URL.";
        public string Fav_Msg_DuplicateTitle => "Duplicado";
        public string Fav_Msg_ConfirmDelete  => "¿Eliminar {0} marcador(es)?";
        public string Fav_Msg_Confirm        => "Confirmar";
        public string Fav_Msg_ImportResult   => "Importación completada:\n\n  ✔ Añadidos:  {0}\n  ⏭ Omitidos: {1} (duplicados)";
        public string Fav_Msg_ImportTitle    => "Importar";
        public string Fav_Msg_ImportJsonError => "Error: {0}";
        public string Fav_Msg_ImportJsonTitle => "Importar JSON";
        public string Fav_Msg_BrowserNotFound => "No se encontraron favoritos de {0}.\n{1}";
        public string Fav_Msg_NotFound       => "No encontrado";
        public string Fav_Msg_FirefoxNotFound => "No se encontró la carpeta de perfiles de Firefox.";
        public string Fav_Msg_FirefoxFallback => "No se puede leer places.sqlite de Firefox (puede que Firefox esté en ejecución).\n\nAlternativa: en Firefox ve a\n  Marcadores ▸ Administrar marcadores ▸ Importar y hacer copia ▸ Exportar marcadores como HTML\ny usa 'Importar HTML' aquí.\n\n¿Abrir Importar HTML ahora?";
        public string Fav_Msg_FirefoxTitle   => "Firefox";
        public string Fav_Msg_SqliteRequired => "Instala el paquete NuGet Microsoft.Data.Sqlite para leer bases de datos de Firefox.";
        public string Fav_Msg_ErrorOpenUrl   => "Error al abrir la URL";
        public string Fav_StatusReady        => "Listo";
        public string Fav_StatusAdded        => "Añadido: {0}";
        public string Fav_StatusEdited       => "Editado: {0}";
        public string Fav_StatusDeleted      => "Eliminado(s): {0}";
        public string Fav_StatusUrlCopied    => "URL copiada al portapapeles";
        public string Fav_StatusExported     => "Exportado {0} → {1}";
        public string Fav_StatusImported     => "Importados {0} marcador(es) — {1} duplicado(s) omitido(s)";
        public string Fav_Count              => "{0} / {1} marcadores";

        // =============================================
        // HISTORYWINDOW
        // =============================================
        public string History_WindowTitle        => "Historial de navegación";
        public string History_Title              => "🕐 Historial";
        public string History_IncognitoNote      => "Los sitios visitados en modo incógnito no aparecen aquí.";
        public string History_DeleteAll          => "🗑 Eliminar todo";
        public string History_Today              => "Hoy";
        public string History_Yesterday          => "Ayer";
        public string History_ThisWeek           => "Esta semana";
        public string History_ThisMonth          => "Este mes";
        public string History_SearchResults      => "Resultados para \"{0}\"";
        public string History_Msg_ConfirmDelete  => "¿Eliminar todo el historial?";
        public string History_Msg_Confirmation   => "Confirmación";

        // =============================================
        // MAINWINDOW
        // =============================================
        public string Main_MaxPanel1            => "Maximizar panel 1";
        public string Main_MaxPanel2            => "Maximizar panel 2";
        public string Main_MaxPanel3            => "Maximizar panel 3";
        public string Main_MaxPanel4            => "Maximizar panel 4";
        public string Main_RestorePanels        => "Restaurar todos los paneles";
        public string Main_Notes                => "Notas";
        public string Main_WelcomePopup         => "⚙ Configura tus páginas de inicio y más...";
        public string Main_FavAdd               => "Agregar a favoritos";
        public string Main_FavManage            => "📁 Gestionar favoritos";
        public string Main_MenuGeneral          => "General";
        public string Main_MenuApariencia       => "Apariencia";
        public string Main_MenuFavoritos        => "Favoritos";
        public string Main_MenuHistorial        => "Historial";
        public string Main_MenuPrivacidad       => "Privacidad";
        public string Main_MenuPermisos         => "Permisos";
        public string Main_MenuCookies          => "Cookies";
        public string Main_MenuAvanzado         => "Avanzado";
        public string Main_MenuAcercaDe         => "Acerca de...";
        public string Main_CtxClearMark         => "Sin marcar";
        public string Main_CtxMarkColor         => "Marcar con color";
        public string Main_CtxSortByColor       => "Ordenar por color";
        public string Main_CtxGoHome            => "Ir a página de inicio";
        public string Main_CtxIncognitoOn       => "Activar modo incógnito";
        public string Main_CtxIncognitoOff      => "Desactivar modo incógnito";
        public string Main_CtxReload            => "Recargar página";
        public string Main_CtxOpenInWindow      => "Abrir en nueva ventana";
        public string Main_CtxOpenWith          => "Abrir con...";
        public string Main_CtxReopenClosed      => "Reabrir pestaña cerrada";
        public string Main_CtxNewTab            => "Nueva pestaña";
        public string Main_CtxNewIncognito      => "Crear pestaña incógnito";
        public string Main_CtxMoveTab           => "Mover pestaña a";
        public string Main_CtxCopyTab           => "Copiar pestaña en";
        public string Main_CtxDuplicate         => "Duplicar pestaña (a la derecha)";
        public string Main_CtxClose             => "Cerrar pestañas";
        public string Main_CtxRestoreSession    => "Restaurar pestañas última sesión";
        public string Main_PosRight             => "A la derecha";
        public string Main_PosLeft              => "A la izquierda";
        public string Main_PosEnd               => "Al final";
        public string Main_PosStart             => "Al principio";
        public string Main_CloseThis            => "Esta";
        public string Main_CloseAllRight        => "Todas a la derecha";
        public string Main_CloseAllLeft         => "Todas a la izquierda";
        public string Main_CloseOthers          => "Todas menos esta";
        public string Main_Browser1             => "Navegador 1";
        public string Main_Browser2             => "Navegador 2";
        public string Main_Browser3             => "Navegador 3";
        public string Main_Browser4             => "Navegador 4";
        public string Main_PermAllow            => "Permitir";
        public string Main_PermBlock            => "Bloquear";
        public string Main_NotesReminderPrefix  => "Tienes ";
        public string Main_NotesReminderSuffix  => " nota(s) con aviso pendiente:\n\n";
        public string Main_NotesReminderQuestion => "\n¿Quieres ver las notas?";
        public string Main_NotesReminderTitle   => "Recordatorio de notas";
        public string Main_NoSession            => "No hay sesión guardada.";
        public string Main_RestoreSessionTitle  => "Restaurar sesión";
        public string Main_SecureNoInfo         => "No hay información de seguridad disponible.";
        public string Main_SecureYes            => "La conexión es segura.\nEl sitio usa HTTPS.";
        public string Main_SecureNo             => "La conexión NO es segura.\nEl sitio no usa HTTPS.";
        public string Main_ErrExternalLink      => "Error abriendo enlace externo: ";
        public string Main_NoTabInit            => "No hay ninguna pestaña inicializada todavía.";
        public string Main_ErrOpenBrowser       => "No se pudo abrir el navegador: ";
        public string Main_AboutTitle           => "Acerca de Multinavigator";
        public string Main_AboutNotAvailable    => "No disponible";
        public string Main_AboutVersion         => "Ver: 7.0.0";
        public string Main_AboutChromium        => "Chromium Ver: ";
        public string Main_AboutLocalIp         => "🏠  IP local: ";
        public string Main_AboutPublicIp        => "🌍  IP pública: ";
        public string Main_AboutAppName         => "Multinavigator 7";
        public string Main_PermFormat           => "{0} quiere usar: {1}";
        public string Main_NewTab               => "Nueva pestaña";
        public string Main_Loading              => "Cargando...";

        // =============================================
        // NOTASWINDOW
        // =============================================
        public string Notas_WindowTitle         => "Notas";
        public string Notas_NewNote             => "+ Nueva nota";
        public string Notas_Reminder            => "Aviso";
        public string Notas_MsgDelete           => "¿Eliminar esta nota?";
        public string Notas_MsgConfirm          => "Confirmar";

        // =============================================
        // THEMEEDITOR
        // =============================================
        public string Theme_WindowTitle         => "Editor de Temas Personalizado";
        public string Theme_HeaderTitle         => "🎨 Editor de Temas";
        public string Theme_PreviewTitle        => "Vista Previa";
        public string Theme_PreviewTab1         => "Pestaña 1";
        public string Theme_PreviewTab2         => "Pestaña 2";
        public string Theme_PreviewIncognito    => "🕵️ Incógnito";
        public string Theme_PreviewUrl          => "🔒 https://ejemplo.com";
        public string Theme_Hue                 => "🎨 Matiz";
        public string Theme_Saturation          => "💧 Saturación";
        public string Theme_Luminosity          => "💡 Luminosidad";
        public string Theme_SecGeneral          => "🎨 Colores Generales";
        public string Theme_SecTabsNormal       => "📑 Pestañas Normales";
        public string Theme_SecNavBar           => "🧭 Barra de Navegación";
        public string Theme_SecButtons          => "🔘 Botones";
        public string Theme_SecButtonCentro     => "🔘 Boton Central";
        public string Theme_SecIncognito        => "🕵️ Pestañas Incógnito";
        public string Theme_ColorWindowBg       => "Fondo de ventana";
        public string Theme_ColorWindowFg       => "Texto de ventana";
        public string Theme_ColorTabInactive    => "Pestaña inactiva";
        public string Theme_ColorTabActive      => "Pestaña activa";
        public string Theme_ColorHover          => "Hover";
        public string Theme_ColorTabActiveHover => "Activa + Hover";
        public string Theme_ColorTabText        => "Texto pestaña";
        public string Theme_ColorNavBarBg       => "Fondo barra";
        public string Theme_ColorNavBarFg       => "Texto";
        public string Theme_ColorUrlBg          => "Fondo URL";
        public string Theme_ColorUrlFg          => "Texto URL";
        public string Theme_ColorButtonAccent   => "Color de botones";
        public string Theme_ColorButtonPressed  => "Presionado";
        public string Theme_ColorButtonCentro   => "Color botón Central";
        public string Theme_ColorIncogInactive  => "Inactiva";
        public string Theme_ColorIncogActive    => "Activa";
        public string Theme_ColorIncogText      => "Texto";
        public string Theme_LabelName           => "Nombre:";
        public string Theme_BtnSave             => "💾 Guardar";
        public string Theme_BtnCancel           => "❌ Cancelar";
        public string Theme_PickerTitle         => "Selector de Color";
        public string Theme_PickerHue           => "Matiz (H)";
        public string Theme_PickerSaturation    => "Saturación (S)";
        public string Theme_PickerLuminosity    => "Luminosidad (L)";
        public string Theme_PickerOk            => "Aceptar";
        public string Theme_PickerCancel        => "Cancelar";
        public string Theme_MsgInitError        => "Error al inicializar el editor: ";
        public string Theme_MsgInitErrorTitle   => "Error";
        public string Theme_MsgNoName           => "Por favor, introduce un nombre para el tema.";
        public string Theme_MsgNoNameTitle      => "Nombre requerido";
        public string Theme_MsgPredefined       => "es un tema predefinido y no puede sobreescribirse.\nElige otro nombre.";
        public string Theme_MsgPredefinedTitle  => "Nombre reservado";
        public string Theme_MsgOverwriteFormat  => "Ya existe un tema personalizado llamado '{0}'.\n¿Quieres sobreescribirlo?";
        public string Theme_MsgOverwriteTitle   => "Tema existente";
        public string Theme_MsgSavedFormat      => "Tema '{0}' guardado correctamente.";
        public string Theme_MsgSavedTitle       => "Tema Guardado";
    }
}
