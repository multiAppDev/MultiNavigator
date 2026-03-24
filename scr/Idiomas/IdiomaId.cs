// Resources/IdiomaId.cs
namespace Multinavigator.Idiomas
{
    internal class IdiomaId : IIdiomaStrings
    {
        public string About_Github            => "Lihat kode di GitHub";
        public string About_Github_button             => "⭐ Beri bintang di GitHub";
        public string Theme_ColorDarkWebContent      => "Konten web dalam mode gelap";
        public static readonly IdiomaId Instance = new();
        // =============================================
        // CONFIGURACION
        // =============================================
        public string Cfg_WindowTitle                    => "Pengaturan";
        public string Cfg_Title                          => "Pengaturan";
        public string Cfg_Language_Label                 => "Bahasa";
        public string Cfg_Language_Title                 => "Bahasa";
        public string Cfg_Language_RestartHint           => "Bahasa akan diterapkan saat setiap jendela dibuka kembali.";

        public string Cfg_TabGeneral                     => "Umum";
        public string Cfg_TabApariencia                  => "Tampilan";
        public string Cfg_TabPrivacidad                  => "Privasi";
        public string Cfg_TabPermisos                    => "Izin";
        public string Cfg_TabAvanzado                    => "Lanjutan";

        public string Cfg_HomePages_Title                => "Halaman beranda";
        public string Cfg_HomePages_Desc                 => "URL yang akan dibuka saat setiap panel dimulai";
        public string Cfg_HomePages_Panel1               => "Panel 1";
        public string Cfg_HomePages_Panel2               => "Panel 2";
        public string Cfg_HomePages_Panel3               => "Panel 3";
        public string Cfg_HomePages_Panel4               => "Panel 4";
        public string Cfg_HomePages_Save                 => "Simpan halaman beranda";

        public string Cfg_Session_Title                  => "Sesi";
        public string Cfg_Session_Restore                => "Pulihkan sesi sebelumnya saat startup";

        public string Cfg_Search_Title                   => "Mesin pencari default";
        public string Cfg_Search_Desc                    => "Digunakan saat mengetik di bilah alamat";
        public string Cfg_Search_Save                    => "Simpan";
        public string Cfg_Search_CustomUrl               => "URL kustom:";

        public string Cfg_Backup_Title                   => "Cadangan";
        public string Cfg_Backup_Desc                    => "Ekspor atau impor pengaturan Anda";
        public string Cfg_Backup_Favorites               => "Favorit";
        public string Cfg_Backup_Themes                  => "Tema";
        public string Cfg_Backup_Permissions             => "Izin";
        public string Cfg_Backup_Settings                => "Pengaturan";
        public string Cfg_Backup_History                 => "Riwayat";
        public string Cfg_Backup_Export                  => "Ekspor pengaturan";
        public string Cfg_Backup_Import                  => "Impor pengaturan";
        public string Cfg_Backup_NoCookies               => "Cookie tidak disertakan dalam cadangan";

        public string Cfg_Appearance_Themes_Title        => "Tema";
        public string Cfg_Appearance_Colors_Title        => "Warna kustom";
        public string Cfg_Appearance_OpenEditor          => "Buka editor tema";
        public string Cfg_Appearance_EditorDesc          => "Buat dan edit tema kustom";
        public string Cfg_Appearance_Preview             => "Pratinjau";
        public string Cfg_Appearance_Tab1                => "Tab";
        public string Cfg_Appearance_Tab2                => "Tab aktif";
        public string Cfg_Appearance_Incognito           => "Penyamaran";
        public string Cfg_Appearance_Options             => "Opsi";
        public string Cfg_Appearance_DarkIncognito       => "Mode gelap otomatis di tab penyamaran";

        public string Cfg_Privacy_Title                  => "Privasi dan data";
        public string Cfg_Privacy_Cookies                => "Cookie";
        public string Cfg_Privacy_CookiesUnit            => "KB di disk";
        public string Cfg_Privacy_CookiesManage          => "Kelola";
        public string Cfg_Privacy_DeleteAll              => "Hapus semua";
        public string Cfg_Privacy_History                => "Riwayat";
        public string Cfg_Privacy_HistoryUnit            => "entri";
        public string Cfg_Privacy_HistoryView            => "Lihat riwayat";
        public string Cfg_Privacy_Cache                  => "Cache";
        public string Cfg_Privacy_CacheUnit              => "MB di disk";
        public string Cfg_Privacy_CacheDelete            => "Hapus cache";

        public string Cfg_Perms_GlobalTitle              => "Izin global";
        public string Cfg_Perms_DomainTitle              => "Izin per domain";
        public string Cfg_Perms_Camera                   => "Kamera";
        public string Cfg_Perms_Mic                      => "Mikrofon";
        public string Cfg_Perms_Location                 => "Lokasi";
        public string Cfg_Perms_Notifications            => "Notifikasi";
        public string Cfg_Perms_DomainCol                => "Domain";
        public string Cfg_Perms_AddDomain                => "Tambah domain";
        public string Cfg_Perms_Reset                    => "Reset izin";

        public string Cfg_Adv_Title                      => "Opsi lanjutan WebView2";
        public string Cfg_Adv_Warning                    => "⚠ Perlu restart aplikasi untuk menerapkan";
        public string Cfg_Adv_BgNetworking               => "Nonaktifkan jaringan latar belakang";
        public string Cfg_Adv_BgNetworking_Desc          => "Blokir koneksi jaringan saat browser di latar belakang";
        public string Cfg_Adv_Sync                       => "Nonaktifkan sinkronisasi";
        public string Cfg_Adv_Sync_Desc                  => "Cegah sinkronisasi data dengan server Google";
        public string Cfg_Adv_Translate                  => "Nonaktifkan penerjemah";
        public string Cfg_Adv_Translate_Desc             => "Hapus layanan terjemahan halaman otomatis";
        public string Cfg_Adv_Extensions                 => "Nonaktifkan ekstensi";
        public string Cfg_Adv_Extensions_Desc            => "Cegah pemuatan ekstensi browser";
        public string Cfg_Adv_DefaultApps                => "Nonaktifkan aplikasi default";
        public string Cfg_Adv_DefaultApps_Desc           => "Cegah instalasi aplikasi default Chrome";
        public string Cfg_Adv_DefaultBrowserCheck        => "Lewati pemeriksaan browser default";
        public string Cfg_Adv_DefaultBrowserCheck_Desc   => "Tidak memeriksa atau meminta untuk menjadi browser default";
        public string Cfg_Adv_Metrics                    => "Catat metrik saja";
        public string Cfg_Adv_Metrics_Desc               => "Metrik dicatat secara lokal tapi tidak dikirim";
        public string Cfg_Adv_Breakpad                   => "Nonaktifkan laporan kesalahan";
        public string Cfg_Adv_Breakpad_Desc              => "Nonaktifkan pengiriman laporan crash otomatis";
        public string Cfg_Adv_Phishing                   => "Nonaktifkan deteksi phishing";
        public string Cfg_Adv_Phishing_Desc              => "Nonaktifkan deteksi situs phishing sisi klien";
        public string Cfg_Adv_HangMonitor                => "Nonaktifkan monitor hang";
        public string Cfg_Adv_HangMonitor_Desc           => "Tidak mendeteksi atau melaporkan browser yang macet";
        public string Cfg_Adv_Repost                     => "Lewati peringatan pengiriman ulang formulir";
        public string Cfg_Adv_Repost_Desc                => "Tidak menampilkan dialog konfirmasi saat refresh POST";
        public string Cfg_Adv_DomainReliability          => "Nonaktifkan telemetri domain";
        public string Cfg_Adv_DomainReliability_Desc     => "Cegah pengiriman data keandalan domain ke Google";
        public string Cfg_Adv_ComponentUpdate            => "Nonaktifkan pembaruan komponen";
        public string Cfg_Adv_ComponentUpdate_Desc       => "Cegah pengunduhan pembaruan komponen otomatis";
        public string Cfg_Adv_BgTimer                    => "Nonaktifkan pembatasan timer";
        public string Cfg_Adv_BgTimer_Desc               => "Timer JS tidak melambat di tab latar belakang";
        public string Cfg_Adv_RendererBg                 => "Nonaktifkan prioritas rendah latar belakang";
        public string Cfg_Adv_RendererBg_Desc            => "Pertahankan prioritas renderer meski tab tersembunyi";
        public string Cfg_Adv_IpcFlood                   => "Nonaktifkan perlindungan banjir IPC";
        public string Cfg_Adv_IpcFlood_Desc              => "Hapus batas pesan IPC antar proses browser";
        public string Cfg_Adv_StateOn                    => "✔ Aktif";
        public string Cfg_Adv_StateOff                   => "✖ Nonaktif";

        public string Cfg_Msg_ConfirmDeleteTheme         => "Hapus tema '{0}'?";
        public string Cfg_Msg_ConfirmDeleteThemeTitle    => "Konfirmasi hapus";
        public string Cfg_Msg_NoTabInitialized           => "Belum ada tab yang diinisialisasi.";
        public string Cfg_Msg_ConfirmDeleteHistory       => "Hapus semua riwayat?";
        public string Cfg_Msg_ConfirmDeleteCookies       => "Hapus semua cookie?";
        public string Cfg_Msg_Confirmation               => "Konfirmasi";
        public string Cfg_Msg_CookiesDeleted             => "Cookie telah dihapus.";
        public string Cfg_Msg_NoActiveTabCache           => "Tidak ada tab aktif untuk membersihkan cache.";
        public string Cfg_Msg_CacheDeleted               => "Cache telah dibersihkan.";
        public string Cfg_Msg_HomePagesSaved             => "Halaman beranda telah disimpan.";
        public string Cfg_Msg_SearchSaved                => "Mesin pencari telah disimpan.";
        public string Cfg_Msg_ExportOk                   => "Pengaturan berhasil diekspor.";
        public string Cfg_Msg_ImportOk                   => "Pengaturan berhasil diimpor.\nRestart aplikasi untuk menerapkan semua perubahan.";
        public string Cfg_Msg_ExportError                => "Kesalahan ekspor: {0}";
        public string Cfg_Msg_ImportError                => "Kesalahan impor: {0}";
        public string Cfg_Msg_Export                     => "Ekspor";
        public string Cfg_Msg_Import                     => "Impor";
        public string Cfg_Msg_Saved                      => "Disimpan";
        public string Cfg_Msg_Done                       => "Selesai";
        public string Cfg_Msg_Error                      => "Kesalahan";
        public string Cfg_Msg_Notice                     => "Pemberitahuan";

        // =============================================
        // COOKIEMANAGERWINDOW
        // =============================================
        public string Cookie_WindowTitle     => "Manajer Cookie";
        public string Cookie_SearchDomain    => "Cari domain:";
        public string Cookie_Reload          => "Muat ulang";
        public string Cookie_Domains         => "Domain";
        public string Cookie_Cookies         => "Cookie";
        public string Cookie_SearchCookie    => "Cari cookie:";
        public string Cookie_ColName         => "Nama";
        public string Cookie_ColValue        => "Nilai";
        public string Cookie_ColPath         => "Jalur";
        public string Cookie_ColExpires      => "Kedaluwarsa";
        public string Cookie_Details         => "Detail";
        public string Cookie_DetailName      => "Nama:";
        public string Cookie_DetailValue     => "Nilai:";
        public string Cookie_DetailDomain    => "Domain:";
        public string Cookie_DetailPath      => "Jalur:";
        public string Cookie_DetailExpires   => "Kedaluwarsa:";
        public string Cookie_DetailFlags     => "Tanda:";
        public string Cookie_FlagsFormat     => "Secure={0}, HttpOnly={1}, Session={2}";
        public string Cookie_CopyValue       => "Salin nilai";
        public string Cookie_EditCookie      => "Edit cookie";
        public string Cookie_DeleteCookie    => "Hapus cookie";
        public string Cookie_DeleteDomain    => "Hapus cookie domain";
        public string Cookie_DeleteAll       => "Hapus SEMUA cookie";
        public string Cookie_Close           => "Tutup";

        // =============================================
        // EDITCOOKIEWINDOW
        // =============================================
        public string EditCookie_WindowTitle     => "Edit Cookie";
        public string EditCookie_Name            => "Nama:";
        public string EditCookie_Value           => "Nilai:";
        public string EditCookie_Path            => "Jalur:";
        public string EditCookie_Expires         => "Tanggal kedaluwarsa:";
        public string EditCookie_Save            => "Simpan";
        public string EditCookie_Cancel          => "Batal";
        public string EditCookie_Msg_EmptyValue  => "Nilai cookie tidak boleh kosong.";
        public string EditCookie_Msg_EmptyPath   => "Jalur tidak boleh kosong.";
        public string EditCookie_Msg_PathSlash   => "Jalur harus dimulai dengan '/'.";
        public string EditCookie_Msg_NoDate      => "Anda harus memilih tanggal kedaluwarsa.";
        public string EditCookie_Msg_PastDate    => "Tanggal kedaluwarsa tidak boleh di masa lalu.";
        public string EditCookie_Msg_SaveError   => "Kesalahan menyimpan cookie: {0}";

        // =============================================
        // FAVORITEEDITDIALOG
        // =============================================
        public string FavDlg_WindowTitle       => "Bookmark";
        public string FavDlg_Title             => "Judul:";
        public string FavDlg_Url               => "URL:";
        public string FavDlg_Folder            => "Grup/Folder:";
        public string FavDlg_Favicon           => "URL Favicon:";
        public string FavDlg_FaviconTooltip    => "Opsional — biarkan kosong untuk deteksi otomatis";
        public string FavDlg_Save              => "Simpan";
        public string FavDlg_Cancel            => "Batal";
        public string FavDlg_Msg_UrlRequired   => "URL wajib diisi.";
        public string FavDlg_Msg_Validation    => "Validasi";

        // =============================================
        // FAVORITESWINDOW
        // =============================================
        public string Fav_WindowTitle        => "Manajer Bookmark";
        public string Fav_Add                => "➕ Tambah";
        public string Fav_Edit               => "✏ Edit";
        public string Fav_Delete             => "🗑 Hapus";
        public string Fav_Up                 => "▲ Atas";
        public string Fav_Down               => "▼ Bawah";
        public string Fav_Open               => "🌐 Buka";
        public string Fav_OpenTooltip        => "Buka di browser (atau klik dua kali baris)";
        public string Fav_CopyUrl            => "📋 Salin URL";
        public string Fav_Close              => "✖ Tutup";
        public string Fav_Export             => "Ekspor:";
        public string Fav_ExportHtml         => "📄 HTML";
        public string Fav_ExportHtmlTooltip  => "Netscape Bookmark HTML — dapat diimpor ke Chrome, Edge, Firefox";
        public string Fav_ExportJson         => "💾 JSON";
        public string Fav_ExportCsv          => "📊 CSV";
        public string Fav_Import             => "Impor:";
        public string Fav_ImportHtml         => "📂 HTML";
        public string Fav_ImportHtmlTooltip  => "Netscape Bookmark HTML (diekspor dari browser manapun)";
        public string Fav_ImportJson         => "📂 JSON";
        public string Fav_ImportFrom         => "Impor bookmark dari:";
        public string Fav_ImportBtn          => "Impor";
        public string Fav_Search             => "🔍 Cari:";
        public string Fav_Group              => "  Grup:";
        public string Fav_Clear              => "Hapus filter";
        public string Fav_AllGroups          => "(Semua)";
        public string Fav_ColTitle           => "Judul";
        public string Fav_ColGroup           => "Grup";
        public string Fav_CtxOpen            => "🌐 Buka di browser";
        public string Fav_CtxEdit            => "✏ Edit";
        public string Fav_CtxCopy            => "📋 Salin URL";
        public string Fav_CtxDelete          => "🗑 Hapus";
        public string Fav_Msg_Duplicate      => "Bookmark dengan URL ini sudah ada.";
        public string Fav_Msg_DuplicateTitle => "Duplikat";
        public string Fav_Msg_ConfirmDelete  => "Hapus {0} bookmark?";
        public string Fav_Msg_Confirm        => "Konfirmasi";
        public string Fav_Msg_ImportResult   => "Impor selesai:\n\n  ✔ Ditambahkan: {0}\n  ⏭ Dilewati:  {1} (duplikat)";
        public string Fav_Msg_ImportTitle    => "Impor";
        public string Fav_Msg_ImportJsonError => "Kesalahan: {0}";
        public string Fav_Msg_ImportJsonTitle => "Impor JSON";
        public string Fav_Msg_BrowserNotFound => "Bookmark tidak ditemukan untuk {0}.\n{1}";
        public string Fav_Msg_NotFound       => "Tidak ditemukan";
        public string Fav_Msg_FirefoxNotFound => "Folder profil Firefox tidak ditemukan.";
        public string Fav_Msg_FirefoxFallback => "Tidak dapat membaca places.sqlite Firefox (Firefox mungkin sedang berjalan).\n\nAlternatif: di Firefox, buka\n  Bookmark ▸ Kelola Bookmark ▸ Impor dan Cadangkan ▸ Ekspor Bookmark ke HTML\nlalu gunakan 'Impor HTML' di sini.\n\nBuka Impor HTML sekarang?";
        public string Fav_Msg_FirefoxTitle   => "Firefox";
        public string Fav_Msg_SqliteRequired => "Instal paket NuGet Microsoft.Data.Sqlite untuk membaca database Firefox.";
        public string Fav_Msg_ErrorOpenUrl   => "Kesalahan membuka URL";
        public string Fav_StatusReady        => "Siap";
        public string Fav_StatusAdded        => "Ditambahkan: {0}";
        public string Fav_StatusEdited       => "Diedit: {0}";
        public string Fav_StatusDeleted      => "Dihapus: {0}";
        public string Fav_StatusUrlCopied    => "URL disalin ke clipboard";
        public string Fav_StatusExported     => "Diekspor {0} → {1}";
        public string Fav_StatusImported     => "{0} bookmark diimpor — {1} duplikat dilewati";
        public string Fav_Count              => "{0} / {1} bookmark";

        // =============================================
        // HISTORYWINDOW
        // =============================================
        public string History_WindowTitle        => "Riwayat Penelusuran";
        public string History_Title              => "🕐 Riwayat";
        public string History_IncognitoNote      => "Situs yang dikunjungi dalam mode penyamaran tidak muncul di sini.";
        public string History_DeleteAll          => "🗑 Hapus semua";
        public string History_Today              => "Hari ini";
        public string History_Yesterday          => "Kemarin";
        public string History_ThisWeek           => "Minggu ini";
        public string History_ThisMonth          => "Bulan ini";
        public string History_SearchResults      => "Hasil untuk \"{0}\"";
        public string History_Msg_ConfirmDelete  => "Hapus semua riwayat?";
        public string History_Msg_Confirmation   => "Konfirmasi";

        // =============================================
        // MAINWINDOW
        // =============================================
        public string Main_MaxPanel1            => "Perbesar panel 1";
        public string Main_MaxPanel2            => "Perbesar panel 2";
        public string Main_MaxPanel3            => "Perbesar panel 3";
        public string Main_MaxPanel4            => "Perbesar panel 4";
        public string Main_RestorePanels        => "Pulihkan semua panel";
        public string Main_Notes                => "Catatan";
        public string Main_WelcomePopup         => "⚙ Atur halaman beranda dan lainnya...";
        public string Main_FavAdd               => "Tambah ke favorit";
        public string Main_FavManage            => "📁 Kelola favorit";
        public string Main_MenuGeneral          => "Umum";
        public string Main_MenuApariencia       => "Tampilan";
        public string Main_MenuFavoritos        => "Favorit";
        public string Main_MenuHistorial        => "Riwayat";
        public string Main_MenuPrivacidad       => "Privasi";
        public string Main_MenuPermisos         => "Izin";
        public string Main_MenuCookies          => "Cookie";
        public string Main_MenuAvanzado         => "Lanjutan";
        public string Main_MenuAcercaDe         => "Tentang...";
        public string Main_CtxClearMark         => "Tanpa tanda";
        public string Main_CtxMarkColor         => "Tandai dengan warna";
        public string Main_CtxSortByColor       => "Urutkan berdasarkan warna";
        public string Main_CtxGoHome            => "Pergi ke beranda";
        public string Main_CtxIncognitoOn       => "Aktifkan mode penyamaran";
        public string Main_CtxIncognitoOff      => "Nonaktifkan mode penyamaran";
        public string Main_CtxReload            => "Muat ulang halaman";
        public string Main_CtxOpenInWindow      => "Buka di jendela baru";
        public string Main_CtxOpenWith          => "Buka dengan...";
        public string Main_CtxReopenClosed      => "Buka kembali tab yang ditutup";
        public string Main_CtxNewTab            => "Tab baru";
        public string Main_CtxNewIncognito      => "Tab penyamaran baru";
        public string Main_CtxMoveTab           => "Pindahkan tab";
        public string Main_CtxCopyTab           => "Salin tab";
        public string Main_CtxDuplicate         => "Duplikasi tab (ke kanan)";
        public string Main_CtxClose             => "Tutup tab";
        public string Main_CtxRestoreSession    => "Pulihkan tab sesi terakhir";
        public string Main_PosRight             => "Ke kanan";
        public string Main_PosLeft              => "Ke kiri";
        public string Main_PosEnd               => "Ke akhir";
        public string Main_PosStart             => "Ke awal";
        public string Main_CloseThis            => "Tab ini";
        public string Main_CloseAllRight        => "Semua di kanan";
        public string Main_CloseAllLeft         => "Semua di kiri";
        public string Main_CloseOthers          => "Semua kecuali ini";
        public string Main_Browser1             => "Browser 1";
        public string Main_Browser2             => "Browser 2";
        public string Main_Browser3             => "Browser 3";
        public string Main_Browser4             => "Browser 4";
        public string Main_PermAllow            => "Izinkan";
        public string Main_PermBlock            => "Blokir";
        public string Main_NotesReminderPrefix  => "Anda memiliki ";
        public string Main_NotesReminderSuffix  => " pengingat catatan yang tertunda:\n\n";
        public string Main_NotesReminderQuestion => "\nApakah Anda ingin melihat catatan?";
        public string Main_NotesReminderTitle   => "Pengingat catatan";
        public string Main_NoSession            => "Tidak ada sesi yang tersimpan.";
        public string Main_RestoreSessionTitle  => "Pulihkan sesi";
        public string Main_SecureNoInfo         => "Informasi keamanan tidak tersedia.";
        public string Main_SecureYes            => "Koneksi aman.\nSitus menggunakan HTTPS.";
        public string Main_SecureNo             => "Koneksi TIDAK AMAN.\nSitus tidak menggunakan HTTPS.";
        public string Main_ErrExternalLink      => "Kesalahan membuka tautan eksternal: ";
        public string Main_NoTabInit            => "Belum ada tab yang diinisialisasi.";
        public string Main_ErrOpenBrowser       => "Tidak dapat membuka browser: ";
        public string Main_AboutTitle           => "Tentang Multinavigator";
        public string Main_AboutNotAvailable    => "Tidak tersedia";
        public string Main_AboutVersion         => "Versi: 7.0.0";
        public string Main_AboutChromium        => "Versi Chromium: ";
        public string Main_AboutLocalIp         => "🏠  IP Lokal: ";
        public string Main_AboutPublicIp        => "🌍  IP Publik: ";
        public string Main_AboutAppName         => "Multinavigator 7";
        public string Main_PermFormat           => "{0} ingin menggunakan: {1}";
        public string Main_NewTab               => "Tab baru";
        public string Main_Loading              => "Memuat...";

        // =============================================
        // NOTASWINDOW
        // =============================================
        public string Notas_WindowTitle         => "Catatan";
        public string Notas_NewNote             => "+ Catatan baru";
        public string Notas_Reminder            => "Pengingat";
        public string Notas_MsgDelete           => "Hapus catatan ini?";
        public string Notas_MsgConfirm          => "Konfirmasi";

        // =============================================
        // THEMEEDITOR
        // =============================================
        public string Theme_WindowTitle         => "Editor Tema Kustom";
        public string Theme_HeaderTitle         => "🎨 Editor Tema";
        public string Theme_PreviewTitle        => "Pratinjau";
        public string Theme_PreviewTab1         => "Tab 1";
        public string Theme_PreviewTab2         => "Tab 2";
        public string Theme_PreviewIncognito    => "🕵️ Penyamaran";
        public string Theme_PreviewUrl          => "🔒 https://contoh.com";
        public string Theme_Hue                 => "🎨 Rona";
        public string Theme_Saturation          => "💧 Saturasi";
        public string Theme_Luminosity          => "💡 Kecerahan";
        public string Theme_SecGeneral          => "🎨 Warna Umum";
        public string Theme_SecTabsNormal       => "📑 Tab Normal";
        public string Theme_SecNavBar           => "🧭 Bilah Navigasi";
        public string Theme_SecButtons          => "🔘 Tombol";
        public string Theme_SecButtonCentro     => "🔘 Tombol Tengah";
        public string Theme_SecIncognito        => "🕵️ Tab Penyamaran";
        public string Theme_ColorWindowBg       => "Latar belakang jendela";
        public string Theme_ColorWindowFg       => "Teks jendela";
        public string Theme_ColorTabInactive    => "Tab tidak aktif";
        public string Theme_ColorTabActive      => "Tab aktif";
        public string Theme_ColorHover          => "Hover";
        public string Theme_ColorTabActiveHover => "Aktif + Hover";
        public string Theme_ColorTabText        => "Teks tab";
        public string Theme_ColorNavBarBg       => "Latar belakang bilah";
        public string Theme_ColorNavBarFg       => "Teks";
        public string Theme_ColorUrlBg          => "Latar belakang URL";
        public string Theme_ColorUrlFg          => "Teks URL";
        public string Theme_ColorButtonAccent   => "Warna tombol";
        public string Theme_ColorButtonPressed  => "Ditekan";
        public string Theme_ColorButtonCentro   => "Warna tombol tengah";
        public string Theme_ColorIncogInactive  => "Tidak aktif";
        public string Theme_ColorIncogActive    => "Aktif";
        public string Theme_ColorIncogText      => "Teks";
        public string Theme_LabelName           => "Nama:";
        public string Theme_BtnSave             => "💾 Simpan";
        public string Theme_BtnCancel           => "❌ Batal";
        public string Theme_PickerTitle         => "Pemilih warna";
        public string Theme_PickerHue           => "Rona (H)";
        public string Theme_PickerSaturation    => "Saturasi (S)";
        public string Theme_PickerLuminosity    => "Kecerahan (L)";
        public string Theme_PickerOk            => "OK";
        public string Theme_PickerCancel        => "Batal";
        public string Theme_MsgInitError        => "Kesalahan inisialisasi editor: ";
        public string Theme_MsgInitErrorTitle   => "Kesalahan";
        public string Theme_MsgNoName           => "Silakan masukkan nama untuk tema.";
        public string Theme_MsgNoNameTitle      => "Nama diperlukan";
        public string Theme_MsgPredefined       => "adalah tema yang telah ditentukan dan tidak dapat ditimpa.\nPilih nama yang berbeda.";
        public string Theme_MsgPredefinedTitle  => "Nama dicadangkan";
        public string Theme_MsgOverwriteFormat  => "Tema kustom bernama '{0}' sudah ada.\nTimpa?";
        public string Theme_MsgOverwriteTitle   => "Tema sudah ada";
        public string Theme_MsgSavedFormat      => "Tema '{0}' berhasil disimpan.";
        public string Theme_MsgSavedTitle       => "Tema disimpan";
    }
}
