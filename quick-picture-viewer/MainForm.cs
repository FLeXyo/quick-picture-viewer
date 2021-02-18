﻿using Microsoft.VisualBasic.FileIO;
using QuickLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace quick_picture_viewer
{
	public partial class MainForm : Form
	{
		private string openPath = null;
		private int zoomFactor = 100;
		private int width = 0;
		private int height = 0;
		private Bitmap originalImage = null;
		private bool autoZoom = true;
		private Point panelMouseDownLocation;
		private bool fullscreen = false;
		private string currentFolder;
		private string selectedFolder;
		private string currentFile;
		private bool alwaysOnTop = false;
		private bool imageChanged = false;
		private bool darkMode = false;
		private bool checkboardBackground = false;
		private bool slideshow = false;
		private int slideshowCounter = 0;
		private bool dragImage = false;
		private System.Timers.Timer zoomInTimer = new System.Timers.Timer();
		private System.Timers.Timer zoomOutTimer = new System.Timers.Timer();
		private System.Timers.Timer slideshowTimer = new System.Timers.Timer();
		private System.Threading.Timer suggestionTimer;
		private NavPanel navPanel = null;
		private bool framelessMode = false;

		public bool printCenterImage = true;
		public bool restartApp = false;

		public MainForm(string openPath, bool darkMode)
		{
			if (darkMode)
			{
				HandleCreated += new EventHandler(ThemeMan.formHandleCreated);
			}

			this.darkMode = darkMode;
			this.openPath = openPath;

			InitializeComponent();

			zoomInTimer.Elapsed += new ElapsedEventHandler(zoomInTimer_Event);
			zoomInTimer.Interval = 100;

			zoomOutTimer.Elapsed += new ElapsedEventHandler(zoomOutTimer_Event);
			zoomOutTimer.Interval = 100;

			slideshowTimer.Elapsed += new ElapsedEventHandler(slideshowTimer_Event);
			slideshowTimer.Interval += 1000;

			picturePanel.MouseWheel += picturePanel_MouseWheel;

			zoomTextBox.TextBox.AutoSize = false;
			zoomTextBox.Height = 21;
			zoomTextBox.TextBox.MouseWheel += TextBox_MouseWheel;

			SetDarkMode(darkMode);
			InitLanguage();

			closeFile();

			toolStrip1.Visible = Properties.Settings.Default.ShowToolbar;
			statusStrip1.Visible = Properties.Settings.Default.ShowStatusBar;

			if (Properties.Settings.Default.StartupRestoreBounds)
			{
				StartPosition = FormStartPosition.Manual;
				Location = Properties.Settings.Default.StartupWindowLocation;

				if (Properties.Settings.Default.StartupWindowSize.Width >= MinimumSize.Width && Properties.Settings.Default.StartupWindowSize.Height >= MinimumSize.Height)
				{
					Size = Properties.Settings.Default.StartupWindowSize;
				}
				else
				{
					Properties.Settings.Default.StartupWindowSize = new Size(700, 485);
				}

			}
			if (Properties.Settings.Default.StartupMaximize)
			{
				WindowState = FormWindowState.Maximized;
			}
		}

		protected override void WndProc(ref Message m)
		{

			base.WndProc(ref m);
			if (m.Msg == NativeMan.WM_SYSCOMMAND)
			{
				if (m.WParam == (IntPtr)NativeMan.SC_MAXIMIZE)
				{
					OnResizeEnd(EventArgs.Empty);
				}
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Left)
			{
				prevFile();
				return true;
			}
			else if (keyData == Keys.Right)
			{
				nextFile();
				return true;
			}
			else if (keyData == Keys.Down)
			{
				zoomOut();
				return true;
			}
			else if (keyData == Keys.Up)
			{
				zoomIn();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void InitLanguage()
		{
			if (string.IsNullOrEmpty(Properties.Settings.Default.Language))
			{
				switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
				{
					case "en":
					case "es":
					case "ru":
					case "cn":
						LangMan.Init("quick_picture_viewer", "quick_picture_viewer.languages.lang_" + CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
						Properties.Settings.Default.Language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
						break;
					default:
						LangMan.Init("quick_picture_viewer");
						Properties.Settings.Default.Language = LangMan.defaultLang;
						break;
				}

				Properties.Settings.Default.Save();
			}
			else
			{
				LangMan.Init("quick_picture_viewer", "quick_picture_viewer.languages.lang_" + Properties.Settings.Default.Language);
			}

			pleaseOpenLabel.Text = LangMan.Get("please-open-image");

			moreButton.Text = LangMan.Get("more-options");
			aboutBtn.Text = LangMan.Get("about") + " ...";
			settingsButton.Text = LangMan.Get("settings") + " ...";
			settingsButton.ShortcutKeyDisplayString = "Ctrl+" + LangMan.Get("comma");
			newWindowButton.Text = LangMan.Get("new-window");
			framelessBtn.Text = LangMan.Get("frameless-mode");
			onTopButton.Text = LangMan.Get("always-on-top");
			backColorBtn.Text = LangMan.Get("background-color");
			backClearBtn.Text = LangMan.Get("clear");
			backCustomBtn.Text = LangMan.Get("choose-color") + " ...";
			actualSizeBtn.Text = LangMan.Get("zoom-to-actual-size") + " (100%)";
			setAsDesktopButton.Text = LangMan.Get("set-as-desktop-background") + " ...";
			printButton.Text = LangMan.Get("print") + " ...";
			deleteBtn.Text = LangMan.Get("move-to-trash") + " ...";
			reloadButton.Text = LangMan.Get("reload-file");

			editButton.Text = LangMan.Get("edit-image");
			flipHorizontalButton.Text = LangMan.Get("flip-horizontal");
			flipVerticalButton.Text = LangMan.Get("flip-vertical");
			rotateRightButton.Text = LangMan.Get("rotate-right");
			rotateLeftButton.Text = LangMan.Get("rotate-left");
			rotate180Button.Text = LangMan.Get("rotate-180");

			copyButton.Text = LangMan.Get("copy");
			copyImageButton.Text = LangMan.Get("copy-image");
			copyFileBtn.Text = LangMan.Get("copy-file");

			externalBtn.Text = LangMan.Get("open-external");
			externalRunBtn.Text = LangMan.Get("open-with-default");
			externalChooseBtn.Text = LangMan.Get("open-with-choose") + " ...";

			openButton.Text = LangMan.Get("open-file") + " | Ctrl+O";
			openFolderButton.Text = LangMan.Get("open-folder") + " | Ctrl+Shift+O";
			saveAsButton.Text = LangMan.Get("save-as") + " | Ctrl+S";
			pasteButton.Text = LangMan.Get("paste-image") + " | Ctrl+V";
			checkboardButton.Text = LangMan.Get("checkboard-background") + " | Ctrl+B";
			fullscreenBtn.Text = LangMan.Get("fullscreen") + " | F";
			miniViewButton.Text = LangMan.Get("picture-in-picture") + " | Ctrl+Shift+P";
			autoZoomButton.Text = LangMan.Get("auto-zoom") + " | Ctrl+A";
			zoomInButton.Text = LangMan.Get("zoom-in") + " | Ctrl+" + LangMan.Get("plus");
			zoomOutButton.Text = LangMan.Get("zoom-out") + " | Ctrl+" + LangMan.Get("minus");
			infoButton.Text = LangMan.Get("image-info") + " | Ctrl+I";
			slideshowButton.Text = LangMan.Get("slideshow") + " | Ctrl+Shift+S";
			showFileButton.Text = LangMan.Get("show-file-explorer") + " | Ctrl+Shift+L";
			prevButton.Text = LangMan.Get("prev-image") + " | " + LangMan.Get("left-arrow");
			nextButton.Text = LangMan.Get("next-image") + " | " + LangMan.Get("right-arrow");

			hasChangesLabel.Text = " " + LangMan.Get("not-saved");
			zoomLabel.Text = " " + LangMan.Get("zoom") + ": " + LangMan.Get("auto");

			effectsBtn.Text = LangMan.Get("effects");
			toolsBtn.Text = LangMan.Get("tools");
			pluginManBtn.Text = LangMan.Get("plugin-manager") + " ...";
			pluginManBtn2.Text = LangMan.Get("plugin-manager") + " ...";

			showMenuItem.Text = LangMan.Get("view");
			showToolbarBtn.Text = LangMan.Get("show-toolbar");
			showStatusBarBtn.Text = LangMan.Get("show-status-bar");

			framelessCloseBtn.Text = NativeMan.GetMessageBoxText(NativeMan.DialogBoxCommandID.IDCLOSE) + " | Alt+F4";
		}

		private void zoomInTimer_Event(Object source, ElapsedEventArgs e)
		{
			zoomIn();
		}

		private void zoomOutTimer_Event(Object source, ElapsedEventArgs e)
		{
			zoomOut();
		}

		private void slideshowTimer_Event(Object source, ElapsedEventArgs e)
		{
			slideshowCounter++;
			if (slideshowCounter >= Properties.Settings.Default.SlideshowTime)
			{
				slideshowCounter = 0;
				nextFile();
			}

			if (Properties.Settings.Default.SlideshowCounter)
			{
				if ((Properties.Settings.Default.SlideshowTime - slideshowCounter) <= 1)
				{
					showSuggestion(LangMan.Get("next-image-in-1-second"), SuggestionIcon.Slideshow);
				}
				else
				{
					showSuggestion(string.Format(LangMan.Get("next-image-in-x-seconds"), Properties.Settings.Default.SlideshowTime - slideshowCounter), SuggestionIcon.Slideshow);
				}
			}
		}

		private void openButton_Click_1(object sender, EventArgs e)
		{
			setSlideshow(false);

			openFileDialog1.Title = LangMan.Get("open-file");

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				openFile(openFileDialog1.FileName);
				selectedFolder = null;
			}
			openFileDialog1.Dispose();
		}

		private void openFolderButton_Click(object sender, EventArgs e)
		{
			setSlideshow(false);

			openFolderDialog1.Description = LangMan.Get("open-folder");

			if(openFolderDialog1.ShowDialog() == DialogResult.OK)
            {
				currentFolder = openFolderDialog1.SelectedPath;
				selectedFolder = openFolderDialog1.SelectedPath;
				openFile(getCurrentFiles(true).FirstOrDefault());
            }
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(openPath))
				{
					ShowNavPanel(false, Properties.Settings.Default.NavPanel);
					if (Properties.Settings.Default.StartupPaste && Clipboard.ContainsImage())
					{
						pasteButton.PerformClick();
						showSuggestion(LangMan.Get("image-pasted-from-clipboard"), SuggestionIcon.Info);
					}
				}
				else
				{
					if (File.GetAttributes(openPath).HasFlag(FileAttributes.Directory))
					{
						currentFolder = openPath;
						openFirstFileInFolder();
					}
					else
					{
						openFile(openPath);
					}
					ShowNavPanel(Properties.Settings.Default.NavPanel, Properties.Settings.Default.NavPanel);
				}
			}
			catch
			{
				showSuggestion(LangMan.Get("unable-open-file"), SuggestionIcon.Warning);
			}

			setAlwaysOnTop(Properties.Settings.Default.AlwaysOnTop, false);
			setCheckboardBackground(Properties.Settings.Default.CheckboardBackground, false);

			if (Properties.Settings.Default.BackColor.Length > 0)
			{
				picturePanel.BackColor = Color.FromArgb(Convert.ToInt32(Properties.Settings.Default.BackColor));
			}

			UpdateMan.Init("ModuleArt", "quick-picture-viewer", "QuickPictureViewer-Setup.exe", darkMode);
			UpdateMan.InitLang(
				LangMan.Get("new-version-available"), 
				LangMan.Get("update-text"), 
				LangMan.Get("install-update"), 
				LangMan.Get("whats-new"),

				LangMan.Get("updating-qpv"),
				LangMan.Get("downloading-update"),
				LangMan.Get("ready-to-install"),
				LangMan.Get("update-failed"),
				LangMan.Get("install")
			);
			UpdateMan.UpdateFailed += UpdateMan_UpdateFailed;
			UpdateMan.IsUpToDate += UpdateMan_IsUpToDate;
			if (Properties.Settings.Default.CheckForUpdates)
			{
				UpdateMan.CheckForUpdates(false, TopMost, Handle);
			}
		}

		private void UpdateMan_IsUpToDate(object sender, EventArgs e)
		{
			showSuggestion(LangMan.Get("app-is-up-to-date"), SuggestionIcon.Check);
		}

		private void UpdateMan_UpdateFailed(object sender, EventArgs e)
		{
			showSuggestion(LangMan.Get("update-failed"), SuggestionIcon.Warning);
		}

		private void showTypeOpsButton(bool show, string type = null)
		{
			typeOpsButton.Invoke((MethodInvoker)(() =>
			{
				typeOpsButton.Visible = show;

				if (show)
				{
					typeOpsButton.Text = " " + type + " " + LangMan.Get("type-options");
					typeOpsButton.Focus();
				}
			}));
		}

		private void openFile(string path)
		{
			try
			{
				showTypeOpsButton(false);
				string ext = Path.GetExtension(path);
				if (ext == ".webp")
				{
					byte[] rawWebP = File.ReadAllBytes(path);
					using (WebP webp = new WebP())
					{
						WebPDecoderOptions decoderOptions = new WebPDecoderOptions();
						decoderOptions.use_threads = 1;
						decoderOptions.alpha_dithering_strength = 100;
						openImage(webp.Decode(rawWebP, decoderOptions), Path.GetDirectoryName(path), Path.GetFileName(path));
					}

					
				}
				else if (ext == ".ico")
				{
					Bitmap bmp = new Icon(path, 128, 128).ToBitmap();
					openImage(bmp, Path.GetDirectoryName(path), Path.GetFileName(path));
				}
				else if (ext == ".svg")
				{
					Bitmap bmp = SvgWrapper.ParseSvg(path);
					switch (SvgWrapper.CurrentError)
					{
						case SvgWrapper.Error.NoError:
							openImage(bmp, Path.GetDirectoryName(path), Path.GetFileName(path));
							showTypeOpsButton(true, SvgWrapper.TypeName);
							break;
						case SvgWrapper.Error.UnableToOpen:
							showSuggestion(SvgWrapper.TypeName + " - " + LangMan.Get("unable-open-file") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
							break;
					}
				}
				else if (ext == ".dds" || ext == ".tga")
				{
					Bitmap bmp = DdsWrapper.ParseDdsOrTga(path);
					switch (DdsWrapper.CurrentError)
					{
						case DdsWrapper.Error.NoError:
							openImage(bmp, Path.GetDirectoryName(path), Path.GetFileName(path));
							break;
						case DdsWrapper.Error.MemoryError:
							showSuggestion(PsdWrapper.TypeName + " - " + LangMan.Get("memory-error") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
							break;
						case DdsWrapper.Error.UnableToOpen:
							showSuggestion(PsdWrapper.TypeName + " - " + LangMan.Get("unable-open-file") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
							break;
						case DdsWrapper.Error.UnsupportedPixelFormat:
							showSuggestion(PsdWrapper.TypeName + " - " + LangMan.Get("unsupported-pixel-format") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
							break;
					}
				}
				else if (ext == ".psd")
				{
					Bitmap bmp = PsdWrapper.ParsePsd(path);
					switch (PsdWrapper.CurrentError)
					{
						case PsdWrapper.Error.NoError:
							openImage(bmp, Path.GetDirectoryName(path), Path.GetFileName(path));
							break;
						case PsdWrapper.Error.UnableToOpen:
							showSuggestion(PsdWrapper.TypeName + " - " + LangMan.Get("unable-open-file") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
							break;
					}
				}
				else if (ext == ".cr2")
				{
					Bitmap bmp = Cr2Wrapper.ParseCr2(path);
					switch (Cr2Wrapper.CurrentError)
					{
						case Cr2Wrapper.Error.NoError:
							openImage(bmp, Path.GetDirectoryName(path), Path.GetFileName(path));
							break;
						case Cr2Wrapper.Error.UnableToOpen:
							showSuggestion(Cr2Wrapper.TypeName + " - " + LangMan.Get("unable-open-file") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
							break;
					}
				}
				else
				{
					if (ext == ".gif")
					{
						openImage(new Bitmap(path), Path.GetDirectoryName(path), Path.GetFileName(path));
					}
					else
					{
						using (Image img = Image.FromFile(path, true))
						{
							openImage(new Bitmap(path), Path.GetDirectoryName(path), Path.GetFileName(path));
						}
					}
				}
			}
			catch (Exception ex)
			{
				showSuggestion(LangMan.Get("unable-open-file") + ": " + Path.GetFileName(path), SuggestionIcon.Warning);
				Console.WriteLine(ex);
			}
		}

		public void openImage(Bitmap bitmap, string directoryName, string fileName)
		{
			if (bitmap != originalImage)
			{
				if (imageChanged)
				{
					DialogResult window = DialogMan.ShowConfirm(
						LangMan.Get("unsaved-changes-question"),
						windowTitle: LangMan.Get("unsaved-changes"),
						yesBtnText: LangMan.Get("save-as"),
						yesBtnImage: saveAsButton.Image,
						showNoBtn: true,
						noBtnText: LangMan.Get("dont-save"),
						noBtnImage: deleteBtn.Image,
						darkMode: darkMode
					);

					if (window == DialogResult.Yes)
					{
						saveAsButton.PerformClick();
					}
					else if (window != DialogResult.No)
					{
						return;
					}
				}

				setImageChanged(false);

				if (originalImage != null)
				{
					originalImage.Dispose();
					originalImage = null;
					pictureBox.Image.Dispose();
					pictureBox.Image = null;
				}

				const int exifOrientationID = 0x112; // 274
				if (bitmap.PropertyIdList.Contains(exifOrientationID))
				{
					var prop = bitmap.GetPropertyItem(exifOrientationID);
					int val = BitConverter.ToUInt16(prop.Value, 0);
					var rot = RotateFlipType.RotateNoneFlipNone;

					if (val == 3 || val == 4)
						rot = RotateFlipType.Rotate180FlipNone;
					else if (val == 5 || val == 6)
						rot = RotateFlipType.Rotate90FlipNone;
					else if (val == 7 || val == 8)
						rot = RotateFlipType.Rotate270FlipNone;

					if (val == 2 || val == 4 || val == 5 || val == 7)
						rot |= RotateFlipType.RotateNoneFlipX;

					if (rot != RotateFlipType.RotateNoneFlipNone)
						bitmap.RotateFlip(rot);
				}

				originalImage = bitmap;
				pictureBox.Image = originalImage;

				width = pictureBox.Image.Size.Width;
				height = pictureBox.Image.Size.Height;
				fileLabel.Text = " " + LangMan.Get("file") + ": " + fileName;

				if (directoryName == null)
				{
					currentFolder = null;
					currentFile = null;
					directoryLabel.Visible = false;
					sizeLabel.Text = " " + LangMan.Get("size") + ": " + width.ToString() + " x " + height.ToString() + " px";
				}
				else
				{
					string path = Path.Combine(directoryName, fileName);

					currentFolder = directoryName;
					currentFile = fileName;
					directoryLabel.Visible = true;
					directoryLabel.Text = " " + LangMan.Get("folder") + ": " + directoryName;
					sizeLabel.Text = " " + LangMan.Get("size") + ": " + width.ToString() + " x " + height.ToString() + " px (" + Converter.PathToSize(path) + ")";

					dateCreatedLabel.Text = " " + LangMan.Get("created") + ": " + File.GetCreationTime(path).ToShortDateString() + " - " + File.GetCreationTime(path).ToLongTimeString();
					dateModifiedLabel.Text = " " + LangMan.Get("modified") + ": " + File.GetLastWriteTime(path).ToShortDateString() + " - " + File.GetLastWriteTime(path).ToLongTimeString();
				}

				nextButton.Enabled = directoryName != null;
				prevButton.Enabled = directoryName != null;
				slideshowButton.Enabled = directoryName != null;
				deleteBtn.Enabled = directoryName != null;
				externalRunBtn.Enabled = directoryName != null;
				externalChooseBtn.Enabled = directoryName != null;
				externalFavoriteBtn.Enabled = directoryName != null;
				showFileButton.Enabled = directoryName != null;
				copyFileBtn.Enabled = directoryName != null;
				reloadButton.Enabled = directoryName != null;
				dateCreatedLabel.Visible = directoryName != null;
				dateModifiedLabel.Visible = directoryName != null;
				ShowNavPanel(directoryName != null && Properties.Settings.Default.NavPanel, Properties.Settings.Default.NavPanel);

				Invoke((MethodInvoker)(() => Text = fileName + " - Quick Picture Viewer"));

				zoomInButton.Enabled = true;
				zoomOutButton.Enabled = true;
				actualSizeBtn.Enabled = true;
				flipVerticalButton.Enabled = true;
				flipHorizontalButton.Enabled = true;
				rotateLeftButton.Enabled = true;
				rotateRightButton.Enabled = true;
				rotate180Button.Enabled = true;
				saveAsButton.Enabled = true;
				copyImageButton.Enabled = true;
				autoZoomButton.Enabled = true;
				setAsDesktopButton.Enabled = true;
				infoButton.Enabled = true;
				printButton.Enabled = true;
				miniViewButton.Enabled = true;

				zoomTextBox.Enabled = true;

				pleaseOpenLabel.Invoke((MethodInvoker)(() => pleaseOpenLabel.Visible = false));

				CheckAutoZoomNeeded();
			}
		}

		private void CheckAutoZoomNeeded()
		{
			if (originalImage != null)
			{
				if (!fullscreen && (originalImage.Width < picturePanel.Width - 32) && (originalImage.Height < picturePanel.Height - 32))
				{
					if (zoomTextBox.Text == "100%")
					{
						setZoomFactor(100);
					}
					else
					{
						setZoomText("100%");
					}
				}
				else
				{
					setZoomText(LangMan.Get("auto"));
				}
			}
			else
			{
				setZoomText(LangMan.Get("auto"));
			}
		}

		private void setImageChanged(bool b, string fakeName = "")
		{
			imageChanged = b;
			hasChangesLabel.Visible = b;

			if (string.IsNullOrEmpty(fakeName))
			{
				Text = currentFile + " - Quick Picture Viewer";
			}
			else
			{
				Text = fakeName + " - Quick Picture Viewer";
			}

			if (b)
			{
				Text = "* " + Text;
			}
		}

		private void zoomInButton_Click(object sender, EventArgs e)
		{
			zoomIn();
		}

		private void zoomOutButton_Click(object sender, EventArgs e)
		{
			zoomOut();
		}

		private void zoomIn()
		{
			if (autoZoom)
			{
				zoomToFit();
			}
			setZoomText((zoomFactor + 5).ToString() + "%");
		}

		private void zoomOut()
		{
			if (autoZoom)
			{
				zoomToFit();
			}
			setZoomText((zoomFactor - 5).ToString() + "%");
		}

		private void zoomToFit()
		{
			double zoomFactorX = picturePanel.Width / (double)originalImage.Width;
			double zoomFactorY = picturePanel.Height / (double)originalImage.Height;

			if (zoomFactorX > zoomFactorY)
			{
				zoomFactor = Convert.ToInt32(zoomFactorY * 100);
			}
			else
			{
				zoomFactor = Convert.ToInt32(zoomFactorX * 100);
			}
		}

		private void setZoomFactor(int newZoomFactor)
		{
			zoomFactor = newZoomFactor;

			zoomLabel.Text = " " + LangMan.Get("zoom") + ": " + zoomFactor.ToString() + "%";

			setAutoZoom(false);

			int newWidth = Convert.ToInt32(width * zoomFactor / 100);
			int newHeight = Convert.ToInt32(height * zoomFactor / 100);

			Size newSize = new Size(newWidth, newHeight);

			pictureBox.Invoke((MethodInvoker)(() =>
			{
				pictureBox.Size = newSize;
				UpdatePictureBoxLocation();
			}));
		}

		private void UpdatePictureBoxLocation()
		{
			if (pictureBox.Width < picturePanel.Width)
			{
				pictureBox.Left = (picturePanel.Width - pictureBox.Width) / 2;
			}
			else
			{
				pictureBox.Left = -picturePanel.HorizontalScroll.Value;
			}

			if (pictureBox.Height < picturePanel.Height)
			{
				pictureBox.Top = (picturePanel.Height - pictureBox.Height) / 2;
			}
			else
			{
				pictureBox.Top = -picturePanel.VerticalScroll.Value;
			}
		}

		private void setZoomText(string text)
		{
			if (!string.IsNullOrEmpty(text) && !zoomTextBox.IsDisposed)
			{
				zoomTextBox.TextBox.Invoke((MethodInvoker)(() => zoomTextBox.Text = text));
			}
		}

		private void setAutoZoom(bool b)
		{
			autoZoom = b;

			autoZoomButton.Checked = b;

			if (b)
			{
				pictureBox.Dock = DockStyle.Fill;

				zoomLabel.Text = " " + LangMan.Get("zoom") + ": " + LangMan.Get("auto");
			}
			else
			{
				pictureBox.Dock = DockStyle.None;
			}
		}

		private void autoZoomButton_Click(object sender, EventArgs e)
		{
			if (zoomTextBox.Text == LangMan.Get("auto"))
			{
				zoomToFit();
				setZoomText(zoomFactor + "%");
			}
			else
			{
				setZoomText(LangMan.Get("auto"));
			}
		}

		private void aboutButton_Click(object sender, EventArgs e)
		{
			AboutForm aboutBox = new AboutForm();
			aboutBox.Owner = this;
			aboutBox.TopMost = alwaysOnTop;
			aboutBox.ShowDialog();
		}

		public void toggleSlideshow()
		{
			setSlideshow(!slideshow);
		}

		private void setSlideshow(bool b)
		{
			slideshow = b;
			slideshowButton.GetCurrentParent().Invoke((MethodInvoker)(() => slideshowButton.Checked = b));

			if (navPanel != null && !navPanel.IsDisposed)
			{
				navPanel.Invoke((MethodInvoker)(() => navPanel.SetSlideshowChecked(b)));
			}

			if (b)
			{
				setFullscreen(b);
				slideshowTimer.Start();
			}
			else
			{
				slideshowTimer.Stop();
			}
		}

		private void setAlwaysOnTop(bool b, bool saveToDisk)
		{
			alwaysOnTop = b;
			TopMost = b;
			onTopButton.Checked = b;

			if (b)
			{
				setFullscreen(false);
			}

			if (saveToDisk)
			{
				Properties.Settings.Default.AlwaysOnTop = b;
				Properties.Settings.Default.Save();
			}
		}

		public void setCheckboardBackground(bool b, bool saveToDisk)
		{
			checkboardBackground = b;
			checkboardButton.Checked = b;

			if (b)
			{
				if (!fullscreen)
				{
					picturePanel.BackColor = Color.Transparent;
					Properties.Settings.Default.BackColor = "";
					Properties.Settings.Default.Save();

					if (darkMode)
					{
						picturePanel.BackgroundImage = Properties.Resources.checkboard_dark;
					}
					else
					{
						picturePanel.BackgroundImage = Properties.Resources.checkboard_light;
					}
				}
			}
			else
			{
				picturePanel.BackgroundImage = null;
			}

			if (saveToDisk)
			{
				Properties.Settings.Default.CheckboardBackground = b;
				Properties.Settings.Default.Save();
			}
		}

		private void flipVerticalButton_Click(object sender, EventArgs e)
		{
			originalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
			pictureBox.Image = originalImage;
			setZoomText(LangMan.Get("auto"));
			setImageChanged(true);
		}

		private void flipHorizontalButton_Click(object sender, EventArgs e)
		{
			originalImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
			pictureBox.Image = originalImage;
			setZoomText(LangMan.Get("auto"));
			setImageChanged(true);
		}

		private void rotateLeftButton_Click(object sender, EventArgs e)
		{
			originalImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
			pictureBox.Image = originalImage;
			setZoomText(LangMan.Get("auto"));
			setImageChanged(true);
		}

		private void rotateRightButton_Click(object sender, EventArgs e)
		{
			originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
			pictureBox.Image = originalImage;
			setZoomText(LangMan.Get("auto"));
			setImageChanged(true);
		}

		private void saveAsButton_Click(object sender, EventArgs e)
		{
			setSlideshow(false);

			if (currentFile != null)
			{
				saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(currentFile);
				saveFileDialog1.InitialDirectory = currentFolder;

				switch (Path.GetExtension(currentFile))
				{
					case ".png":
						saveFileDialog1.FilterIndex = 1;
						break;
					case ".jpg":
					case ".jpeg":
					case ".jpe":
					case ".jfif":
					case ".exif":
						saveFileDialog1.FilterIndex = 2;
						break;
					case ".gif":
						saveFileDialog1.FilterIndex = 3;
						break;
					case ".bmp":
					case ".dib":
					case ".rle":
						saveFileDialog1.FilterIndex = 4;
						break;
					case ".tiff":
					case ".tif":
						saveFileDialog1.FilterIndex = 5;
						break;
					case ".ico":
						saveFileDialog1.FilterIndex = 6;
						break;
					case ".webp":
						saveFileDialog1.FilterIndex = 7;
						break;
				}
			}

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				FileStream fs = (FileStream)saveFileDialog1.OpenFile();
				switch (saveFileDialog1.FilterIndex)
				{
					case 1:
						originalImage.Save(fs, ImageFormat.Png);
						break;
					case 2:
						originalImage.Save(fs, ImageFormat.Jpeg);
						break;
					case 3:
						originalImage.Save(fs, ImageFormat.Gif);
						break;
					case 4:
						originalImage.Save(fs, ImageFormat.Bmp);
						break;
					case 5:
						originalImage.Save(fs, ImageFormat.Tiff);
						break;
					case 6:
						IcoWrapper.ConvertToIcon(originalImage, fs);
						break;
					case 7:
						using (WebP webp = new WebP())
						{
							byte[] rawWebP = webp.EncodeLossy(originalImage);
							fs.Write(rawWebP, 0, rawWebP.Length);
						}
						break;
				}
				fs.Close();

				setImageChanged(false);
				openFile(saveFileDialog1.FileName);
			}
			saveFileDialog1.Dispose();
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			Clipboard.SetImage(originalImage);
			showSuggestion(LangMan.Get("image-copied-to-clipboard"), SuggestionIcon.Check);
		}

		private void pasteButton_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				openImage(new Bitmap(Clipboard.GetImage()), null, LangMan.Get("from-clipboard"));
				setImageChanged(true, LangMan.Get("from-clipboard"));
			}
			else if (Clipboard.ContainsData(DataFormats.FileDrop))
			{
				string path = ((string[])Clipboard.GetData(DataFormats.FileDrop))[0];
				openFile(path);
			}
		}

		private void picturePanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (autoZoom)
				{
					AllowDrop = false;
					if (currentFile != null || originalImage != null)
					{
						dragImage = true;
					}
				}
				else
				{
					Cursor.Current = Cursors.SizeAll;

					panelMouseDownLocation = new Point(
						PointToClient(Cursor.Position).X + picturePanel.HorizontalScroll.Value,
						PointToClient(Cursor.Position).Y + picturePanel.VerticalScroll.Value
					);
				}
			}
		}

		private void picturePanel_MouseUp(object sender, MouseEventArgs e)
		{
			AllowDrop = true;
			Cursor.Current = Cursors.Default;
		}

		private void picturePanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (autoZoom)
				{
					if (dragImage)
					{
						if (currentFile != null)
						{
							string[] paths = { Path.Combine(currentFolder, currentFile) };
							DoDragDrop(new DataObject(DataFormats.FileDrop, paths), DragDropEffects.Copy);
						}
						else if (originalImage != null)
						{
							DoDragDrop(originalImage, DragDropEffects.Copy);
						}
						dragImage = false;
					}
				}
				else
				{
					int newX = panelMouseDownLocation.X - PointToClient(Cursor.Position).X;
					int newY = panelMouseDownLocation.Y - PointToClient(Cursor.Position).Y;

					if (newX > picturePanel.HorizontalScroll.Minimum)
					{
						if (newX < picturePanel.HorizontalScroll.Maximum)
						{
							picturePanel.HorizontalScroll.Value = newX;
						}
						else
						{
							picturePanel.HorizontalScroll.Value = picturePanel.HorizontalScroll.Maximum;
						}
					}
					else
					{
						picturePanel.HorizontalScroll.Value = picturePanel.HorizontalScroll.Minimum;
					}

					if (newY > picturePanel.VerticalScroll.Minimum)
					{
						if (newY < picturePanel.VerticalScroll.Maximum)
						{
							picturePanel.VerticalScroll.Value = newY;
						}
						else
						{
							picturePanel.VerticalScroll.Value = picturePanel.VerticalScroll.Maximum;
						}
					}
					else
					{
						picturePanel.VerticalScroll.Value = picturePanel.VerticalScroll.Minimum;
					}

					picturePanel.Update();
				}
			}
		}

		private void picturePanel_MouseWheel(object sender, MouseEventArgs e)
		{
			if (originalImage != null)
			{
				if (Control.ModifierKeys == Keys.Control || Properties.Settings.Default.MouseWheelScrollAction == 1)
				{
					if (e.Delta > 0)
					{
						zoomIn();
					}
					else if (e.Delta < 0)
					{
						zoomOut();
					}
				}
				else if (Properties.Settings.Default.MouseWheelScrollAction == 2)
				{
					if (e.Delta > 0)
					{
						prevFile();
					}
					else if (e.Delta < 0)
					{
						nextFile();
					}
				}
			}
		}

		private void TextBox_MouseWheel(object sender, MouseEventArgs e)
		{
			if (originalImage != null)
			{
				if (e.Delta > 0)
				{
					zoomIn();
				}
				else if (e.Delta < 0)
				{
					zoomOut();
				}
			}
		}

		private void fullscreenButton_Click(object sender, EventArgs e)
		{
			setFullscreen(!fullscreen);
		}

		private void UpdatePicturePanelHeight()
		{
			int topMargin = 0;
			if (toolStrip1.Visible)
			{
				topMargin += toolStrip1.Height;
			}

			int bottomMargin = 0;
			if (statusStrip1.Visible)
			{
				bottomMargin += statusStrip1.Height;
			}

			picturePanel.Top = topMargin;
			picturePanel.Height = ClientSize.Height - topMargin - bottomMargin;
		}

		private void setFullscreen(bool b)
		{
			OnResizeBegin(EventArgs.Empty);

			fullscreen = b;

			WindowState = FormWindowState.Normal;

			if (b)
			{
				toolStrip1.Visible = false;
				statusStrip1.Visible = false;

				if (!Properties.Settings.Default.ShowCursorInFullscreen)
				{
					Bitmap bitmap = new Bitmap(1, 1);
					IntPtr ptr = bitmap.GetHicon();
					picturePanel.Cursor = new Cursor(ptr);
				}

				picturePanel.BackColor = Color.Black;

				typeOpsButton.Left = ClientRectangle.Width + 27;
				pleaseOpenLabel.ForeColor = Color.White;

				FormBorderStyle = FormBorderStyle.None;
				WindowState = FormWindowState.Maximized;

				if (!Properties.Settings.Default.NavPanelInFullscreen && navPanel != null && !navPanel.IsDisposed)
				{
					navPanel.Visible = false;
				}

				setAlwaysOnTop(false, true);

				showSuggestion(string.Format(LangMan.Get("press-to-exit-fullscreen"), "Esc"), SuggestionIcon.Fullscreen);
			}
			else
			{
				toolStrip1.Visible = Properties.Settings.Default.ShowToolbar;
				statusStrip1.Visible = Properties.Settings.Default.ShowStatusBar;

				picturePanel.Cursor = Cursors.Default;

				typeOpsButton.Left = ClientRectangle.Width - typeOpsButton.Width - 27;
				pleaseOpenLabel.ForeColor = ForeColor;

				if (!framelessMode)
				{
					FormBorderStyle = FormBorderStyle.Sizable;
				}

				if (checkboardBackground)
				{
					picturePanel.BackColor = Color.Transparent;
					setCheckboardBackground(true, true);
				}
				else
				{
					if (Properties.Settings.Default.BackColor.Length > 0)
					{
						picturePanel.BackColor = Color.FromArgb(Convert.ToInt32(Properties.Settings.Default.BackColor));
					}
					else
					{
						picturePanel.BackColor = Color.Transparent;
					}
				}

				if (navPanel != null && !navPanel.IsDisposed)
				{
					navPanel.Visible = true;
				}
			}

			CheckAutoZoomNeeded();

			OnResizeEnd(EventArgs.Empty);
		}

		private void zoomComboBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (zoomTextBox.Text == LangMan.Get("auto"))
				{
					setAutoZoom(true);
				}
				else
				{
					string substr = zoomTextBox.Text.Replace("%", "");
					int zoom = int.Parse(substr);

					if (zoom < 1)
					{
						zoom = 1;
						setZoomText(zoom.ToString() + "%");
					}
					else
					{
						if (zoom > 1000)
						{
							zoom = 1000;
							setZoomText(zoom.ToString() + "%");
						}
						else
						{
							setZoomFactor(zoom);
						}
					}
				}
			}
			catch
			{

			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (!zoomTextBox.Focused)
			{
				if (e.Control)
				{
					if (e.Shift)
					{
						if (e.KeyCode == Keys.L)
						{
							showFileButton.PerformClick();
						}
						else if (e.KeyCode == Keys.S)
						{
							toggleSlideshow();
						}
						else if (e.KeyCode == Keys.P)
						{
							miniViewButton.PerformClick();
						}
						else if (e.KeyCode == Keys.O)
                        {
							openFolderButton.PerformClick();
                        }
					}
					else
					{
						if (e.KeyCode == Keys.O)
						{
							openButton.PerformClick();
						}
						else if (e.KeyCode == Keys.B)
						{
							checkboardButton.PerformClick();
						}
						else if (e.KeyCode == Keys.S)
						{
							saveAsButton.PerformClick();
						}
						else if (e.KeyCode == Keys.A)
						{
							autoZoomButton.PerformClick();
						}
						else if (e.KeyCode == Keys.V)
						{
							pasteButton.PerformClick();
						}
						else if (e.KeyCode == Keys.Oemplus)
						{
							zoomInButton.PerformClick();
						}
						else if (e.KeyCode == Keys.OemMinus)
						{
							zoomOutButton.PerformClick();
						}
						else if (e.KeyCode == Keys.T)
						{
							onTopButton.PerformClick();
						}
						else if (e.KeyCode == Keys.I)
						{
							infoButton.PerformClick();
						}
					}
				}
				else if (e.Alt)
				{
					if (e.KeyCode == Keys.Enter)
					{
						fullscreenBtn.PerformClick();
					}
					else if (e.KeyCode == Keys.Down)
					{
						int newVerticalValue = picturePanel.VerticalScroll.Value + picturePanel.VerticalScroll.LargeChange;

						if (newVerticalValue >= picturePanel.VerticalScroll.Maximum)
						{
							picturePanel.VerticalScroll.Value = picturePanel.VerticalScroll.Maximum;
						}
						else
						{
							picturePanel.VerticalScroll.Value = newVerticalValue;
						}
					}
					else if (e.KeyCode == Keys.Up)
					{
						int newVerticalValue = picturePanel.VerticalScroll.Value - picturePanel.VerticalScroll.LargeChange;

						if (newVerticalValue <= picturePanel.VerticalScroll.Minimum)
						{
							picturePanel.VerticalScroll.Value = picturePanel.VerticalScroll.Minimum;
						}
						else
						{
							picturePanel.VerticalScroll.Value = newVerticalValue;
						}
					}
					else if (e.KeyCode == Keys.Left)
					{
						int newHorizontalValue = picturePanel.HorizontalScroll.Value - picturePanel.HorizontalScroll.LargeChange;

						if (newHorizontalValue <= picturePanel.HorizontalScroll.Minimum)
						{
							picturePanel.HorizontalScroll.Value = picturePanel.HorizontalScroll.Minimum;
						}
						else
						{
							picturePanel.HorizontalScroll.Value = newHorizontalValue;
						}
					}
					else if (e.KeyCode == Keys.Right)
					{
						int newHorizontalValue = picturePanel.HorizontalScroll.Value + picturePanel.HorizontalScroll.LargeChange;

						if (newHorizontalValue >= picturePanel.HorizontalScroll.Maximum)
						{
							picturePanel.HorizontalScroll.Value = picturePanel.HorizontalScroll.Maximum;
						}
						else
						{
							picturePanel.HorizontalScroll.Value = newHorizontalValue;
						}
					}
				}
				else
				{
					if (e.KeyCode == Keys.F || e.KeyCode == Keys.F11)
					{
						fullscreenBtn.PerformClick();
					}
					else if (e.KeyCode == Keys.Escape)
					{
						if (!fullscreen && Properties.Settings.Default.EscToExit)
						{
							Close();
						}
						setFullscreen(false);
					}
					else if (e.KeyCode == Keys.F7)
					{
						showToolbarBtn.PerformClick();
					}
					else if (e.KeyCode == Keys.F8)
					{
						showStatusBarBtn.PerformClick();
					}
				}
			}
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			Bitmap bitmap = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];

			if (bitmap != null)
			{
				openImage(bitmap, null, LangMan.Get("dragged-image"));
				setImageChanged(true, LangMan.Get("dragged-image"));
			}
			else if (files.Length > 0)
			{
				string path = files[0];
				openFile(path);
			}
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Bitmap))
			{
				e.Effect = DragDropEffects.All;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void onTopButton_Click(object sender, EventArgs e)
		{
			setAlwaysOnTop(!alwaysOnTop, true);
		}

		public int nextFile()
		{

			string[] filePaths = selectedFolder != null ? getCurrentFiles(true) : getCurrentFiles();

			int currentIndex = -1;
			for (int i = 0; i < filePaths.Length; i++)
			{
				if (filePaths[i].ToLower() == Path.Combine(currentFolder, currentFile).ToLower())
				{
					currentIndex = i;
					break;
				}
			}

			if (currentIndex == -1)
			{
				setSlideshow(false);
				showSuggestion(LangMan.Get("cur-file-not-found"), SuggestionIcon.Warning);
				return 0;
			}
			else
			{
				if (currentIndex == filePaths.Length - 1)
				{
					openFile(filePaths[0]);
				}
				else
				{
					openFile(filePaths[currentIndex + 1]);
				}

				return filePaths.Length;
			}
		}

		private void nextButton_Click(object sender, EventArgs e)
		{
			nextFile();
		}

		public void prevFile()
		{
			string[] filePaths = selectedFolder != null ? getCurrentFiles(true) : getCurrentFiles();

			int currentIndex = -1;
			for (int i = 0; i < filePaths.Length; i++)
			{
				if (filePaths[i].ToLower() == Path.Combine(currentFolder, currentFile).ToLower())
				{
					currentIndex = i;
					break;
				}
			}

			if (currentIndex == -1)
			{
				showSuggestion(LangMan.Get("cur-file-not-found"), SuggestionIcon.Warning);
			}
			else
			{
				if (currentIndex == 0)
				{
					openFile(filePaths[filePaths.Length - 1]);
				}
				else
				{
					openFile(filePaths[currentIndex - 1]);
				}
			}
		}

		private void prevButton_Click(object sender, EventArgs e)
		{
			prevFile();
		}

		private string[] getCurrentFiles(bool recursive = false)
		{
			string[] exts = { ".png", ".jpg", ".jpeg", ".jpe", ".jfif", ".exif", ".gif", ".bmp", ".dib", ".rle", ".ico", ".webp", ".svg", ".dds", ".tga", ".psd" };
			List<string> arlist = new List<string>();

			if (currentFolder != null)
			{
				string[] allFiles = recursive ? Directory.GetFiles(selectedFolder, "*", System.IO.SearchOption.AllDirectories) : Directory.GetFiles(currentFolder);
				for (int i = 0; i < allFiles.Length; i++)
				{
					string ext = Path.GetExtension(allFiles[i]).ToLower();
					if (exts.Contains(ext))
					{
						arlist.Add(allFiles[i]);
					}
				}
			}

			return arlist.ToArray();
		}

		private void openFirstFileInFolder()
		{
			string[] filePaths = getCurrentFiles();

			if (filePaths.Length > 0)
			{
				openFile(filePaths[0]);
			}
			else
			{
				showSuggestion(LangMan.Get("no-files-to-open"), SuggestionIcon.Warning);
			}
		}

		private void setAsDesktopButton_Click(object sender, EventArgs e)
		{
			WallpaperForm wallpaperForm = new WallpaperForm(originalImage);
			wallpaperForm.Owner = this;
			wallpaperForm.TopMost = alwaysOnTop;
			wallpaperForm.DarkMode = darkMode;
			wallpaperForm.ShowDialog();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			DialogResult d = DialogMan.ShowConfirm(
				LangMan.Get("sure-move-to-trash"),
				yesBtnImage: deleteBtn.Image,
				windowTitle: LangMan.Get("delete-file"),
				darkMode: darkMode
			);

			if (d == DialogResult.Yes)
			{
				string path = Path.Combine(currentFolder, currentFile);
				if (FileSystem.FileExists(path))
				{
					originalImage.Dispose();
					originalImage = null;
					pictureBox.Image.Dispose();
					pictureBox.Image = null;

					if (nextFile() <= 1)
					{
						closeFile();
					}
					FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
				}
				else
				{
					showSuggestion(LangMan.Get("cur-file-not-found"), SuggestionIcon.Warning);
				}
			}
		}

		private void closeFile()
		{
			if (originalImage != null)
			{
				originalImage.Dispose();
				originalImage = null;
			}

			if (pictureBox.Image != null)
			{
				pictureBox.Image.Dispose();
				pictureBox.Image = null;
			}

			saveAsButton.Enabled = false;
			deleteBtn.Enabled = false;
			prevButton.Enabled = false;
			nextButton.Enabled = false;
			slideshowButton.Enabled = false;
			autoZoomButton.Enabled = false;
			zoomInButton.Enabled = false;
			zoomOutButton.Enabled = false;
			actualSizeBtn.Enabled = false;
			rotateLeftButton.Enabled = false;
			rotateRightButton.Enabled = false;
			rotate180Button.Enabled = false;
			flipHorizontalButton.Enabled = false;
			flipVerticalButton.Enabled = false;
			copyImageButton.Enabled = false;
			copyFileBtn.Enabled = false;
			setAsDesktopButton.Enabled = false;
			reloadButton.Enabled = false;
			infoButton.Enabled = false;
			externalRunBtn.Enabled = false;
			externalChooseBtn.Enabled = false;
			externalFavoriteBtn.Enabled = false;
			printButton.Enabled = false;
			showFileButton.Enabled = false;
			miniViewButton.Enabled = false;
			ShowNavPanel(false, Properties.Settings.Default.NavPanel);

			zoomTextBox.Enabled = false;

			pleaseOpenLabel.Visible = true;

			directoryLabel.Visible = false;
			fileLabel.Text = " " + LangMan.Get("no-file");
			sizeLabel.Text = " " + LangMan.Get("size") + ": 0 x 0 px";
			dateCreatedLabel.Visible = false;
			dateModifiedLabel.Visible = false;

			setZoomText(LangMan.Get("auto"));
			setSlideshow(false);
			setFullscreen(false);
		}

		private void infoButton_Click(object sender, EventArgs e)
		{
			InfoForm infoForm = new InfoForm(originalImage, currentFolder, currentFile);
			infoForm.Owner = this;
			infoForm.TopMost = alwaysOnTop;
			infoForm.DarkMode = darkMode;
			infoForm.ShowDialog();
		}

		private void picturePanel_DoubleClick(object sender, EventArgs e)
		{
			fullscreenBtn.PerformClick();
		}

		private void showOpenWithDialog(string path)
		{
			string args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
			args += ",OpenAs_RunDLL " + path;
			Process.Start("rundll32.exe", args);
		}

		private void externalButton_Click(object sender, EventArgs e)
		{
			showOpenWithDialog(Path.Combine(currentFolder, currentFile));
		}

		private void SetDarkMode(bool dark)
		{
			if (dark)
			{
				ThemeMan.setDarkModeToControl(picturePanel.Handle);

				ForeColor = Color.White;
				BackColor = ThemeMan.DarkBackColor;
				statusStrip1.BackColor = ThemeMan.DarkSecondColor;

				openButton.Image = Properties.Resources.white_open;
				openFolderButton.Image = Properties.Resources.white_picfolder;
				saveAsButton.Image = Properties.Resources.white_saveas;
				printButton.Image = Properties.Resources.white_print;
				deleteBtn.Image = Properties.Resources.white_trash;

				externalBtn.Image = Properties.Resources.white_popup;
				externalRunBtn.Image = Properties.Resources.white_exe;
				externalChooseBtn.Image = Properties.Resources.white_list;
				externalFavoriteBtn.Image = Properties.Resources.white_paint;

				prevButton.Image = Properties.Resources.white_prev;
				showFileButton.Image = Properties.Resources.white_picfolder;
				nextButton.Image = Properties.Resources.white_next;
				slideshowButton.Image = Properties.Resources.white_slideshow;

				autoZoomButton.Image = Properties.Resources.white_autozoom;
				zoomInButton.Image = Properties.Resources.white_zoomin;
				zoomOutButton.Image = Properties.Resources.white_zoomout;
				actualSizeBtn.Image = Properties.Resources.white_actualsize;

				editButton.Image = Properties.Resources.white_edit;
				rotateLeftButton.Image = Properties.Resources.white_rotatel;
				rotateRightButton.Image = Properties.Resources.white_rotater;
				flipHorizontalButton.Image = Properties.Resources.white_fliph;
				flipVerticalButton.Image = Properties.Resources.white_flipv;
				rotate180Button.Image = Properties.Resources.white_degree;
				customAngleBtn.Image = Properties.Resources.white_angle;

				infoButton.Image = Properties.Resources.white_info;
				copyButton.Image = Properties.Resources.white_copy;
				copyImageButton.Image = Properties.Resources.white_image;
				copyFileBtn.Image = Properties.Resources.white_imgfile;
				pasteButton.Image = Properties.Resources.white_paste;

				checkboardButton.Image = Properties.Resources.white_grid;
				fullscreenBtn.Image = Properties.Resources.white_fullscreen;
				onTopButton.Image = Properties.Resources.white_ontop;
				miniViewButton.Image = Properties.Resources.white_miniview;

				moreButton.Image = Properties.Resources.white_more;
				setAsDesktopButton.Image = Properties.Resources.white_desktop;
				aboutBtn.Image = Properties.Resources.white_about;
				reloadButton.Image = Properties.Resources.white_sync;
				newWindowButton.Image = Properties.Resources.white_newwindow;
				settingsButton.Image = Properties.Resources.white_settings;
				backColorBtn.Image = Properties.Resources.white_palette;
				backClearBtn.Image = Properties.Resources.white_erase;
				backCustomBtn.Image = Properties.Resources.white_palette;
				framelessBtn.Image = Properties.Resources.white_frame;

				directoryLabel.Image = Properties.Resources.white_picfolder;
				fileLabel.Image = Properties.Resources.white_imgfile;
				sizeLabel.Image = Properties.Resources.white_size;
				zoomLabel.Image = Properties.Resources.white_zoom;
				dateCreatedLabel.Image = Properties.Resources.white_clock;
				dateModifiedLabel.Image = Properties.Resources.white_history;
				hasChangesLabel.Image = Properties.Resources.white_erase;

				typeOpsButton.Image = Properties.Resources.white_options;
				typeOpsButton.BackColor = ThemeMan.DarkSecondColor;
				typeOpsButton.ForeColor = Color.White;

				effectsBtn.Image = Properties.Resources.white_effects;
				toolsBtn.Image = Properties.Resources.white_tools;
				pluginManBtn.Image = Properties.Resources.white_plugin;
				pluginManBtn2.Image = Properties.Resources.white_plugin;

				framelessCloseBtn.Image = Properties.Resources.white_close;

				zoomTextBox.BackColor = ThemeMan.DarkMainColor;
				zoomTextBox.ForeColor = Color.White;

				rmbMenu.SetDarkMode(dark);
				showMenuItem.Image = Properties.Resources.white_show;
				showMenuItem.DropDown.BackColor = ThemeMan.DarkSecondColor;
				showToolbarBtn.Image = Properties.Resources.white_toolbar;
				showStatusBarBtn.Image = Properties.Resources.white_statusbar;
			}

			toolStrip1.DarkMode = dark;
		}

		private void printButton_Click(object sender, EventArgs e)
		{
			setSlideshow(false);

			string docTitle = LangMan.Get("image") + " 1";
			if (currentFile != null)
			{
				docTitle = currentFile;
			}

			printDocument1.DocumentName = docTitle;
			printDocument1.PrinterSettings.PrintFileName = docTitle;

			PrintForm pf = new PrintForm(printDocument1, docTitle);
			pf.Owner = this;
			pf.TopMost = alwaysOnTop;
			pf.DarkMode = darkMode;

			if (pf.ShowDialog() == DialogResult.OK)
			{
				if (printDialog1.ShowDialog() == DialogResult.OK)
				{
					printDocument1.Print();
				}
			}
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			RectangleF marginBounds = e.MarginBounds;
			RectangleF printableArea = e.PageSettings.PrintableArea;

			int availableWidth = (int)Math.Floor(printDocument1.OriginAtMargins
				? marginBounds.Width
				: (e.PageSettings.Landscape
					? printableArea.Height
					: printableArea.Width));

			int availableHeight = (int)Math.Floor(printDocument1.OriginAtMargins
				? marginBounds.Height
				: (e.PageSettings.Landscape
					? printableArea.Width
					: printableArea.Height));

			if (availableWidth > originalImage.Width && availableHeight > originalImage.Height)
			{
				int x = 0;
				int y = 0;
				if (printCenterImage)
				{
					x = (availableWidth - originalImage.Width) / 2;
					y = (availableHeight - originalImage.Height) / 2;
				}
				e.Graphics.DrawImage(originalImage, x, y, originalImage.Width, originalImage.Height);
			}
			else
			{
				double scaleW = availableWidth / (double)originalImage.Width;
				double scaleH = availableHeight / (double)originalImage.Height;

				if (scaleW < scaleH)
				{
					int x = 0;
					int y = 0;
					if (printCenterImage)
					{
						x = (availableWidth - Convert.ToInt32(originalImage.Width * scaleW)) / 2;
						y = (availableHeight - Convert.ToInt32(originalImage.Height * scaleW)) / 2;
					}
					e.Graphics.DrawImage(originalImage, x, y, availableWidth, Convert.ToInt32(originalImage.Height * scaleW));
				}
				else
				{
					int x = 0;
					int y = 0;
					if (printCenterImage)
					{
						x = (availableWidth - Convert.ToInt32(originalImage.Width * scaleH)) / 2;
						y = (availableHeight - Convert.ToInt32(originalImage.Height * scaleH)) / 2;
					}
					e.Graphics.DrawImage(originalImage, x, y, Convert.ToInt32(originalImage.Width * scaleH), availableHeight);
				}
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (imageChanged)
			{
				DialogResult window = DialogMan.ShowConfirm(
					LangMan.Get("unsaved-changes-question"),
					windowTitle: LangMan.Get("unsaved-changes"),
					yesBtnText: LangMan.Get("save-as"),
					yesBtnImage: saveAsButton.Image,
					showNoBtn: true,
					noBtnText: LangMan.Get("dont-save"),
					noBtnImage: deleteBtn.Image,
					darkMode: darkMode
				);

				if (window == DialogResult.Yes)
				{
					saveAsButton.PerformClick();
				}
				else if (window != DialogResult.No)
				{
					e.Cancel = true;
				}
			}

			if (Properties.Settings.Default.StartupRestoreBounds)
			{
				if (WindowState == FormWindowState.Maximized)
				{
					Properties.Settings.Default.StartupMaximize = true;
				}
				else
				{
					Properties.Settings.Default.StartupMaximize = false;
					Properties.Settings.Default.StartupWindowLocation = Location;
					Properties.Settings.Default.StartupWindowSize = Size;
				}
				Properties.Settings.Default.Save();
			}
		}

		private void showFileButton_Click(object sender, EventArgs e)
		{
			string path = Path.Combine(currentFolder, currentFile);
			if (FileSystem.FileExists(path))
			{
				string argument = "/select, \"" + path + "\"";
				Process.Start("explorer.exe", argument);
			}
			else
			{
				showSuggestion(LangMan.Get("cur-file-not-found"), SuggestionIcon.Warning);
			}
		}

		private void checkboardButton_Click(object sender, EventArgs e)
		{
			setCheckboardBackground(!checkboardBackground, true);
		}

		private void zoomInButton_MouseDown(object sender, MouseEventArgs e)
		{
			zoomInTimer.Start();
		}

		private void zoomOutButton_MouseDown(object sender, MouseEventArgs e)
		{
			zoomOutTimer.Start();
		}

		public enum SuggestionIcon
		{
			Info = 0,
			Check = 1,
			Warning = 2,
			Slideshow = 3,
			Fullscreen = 4
		}

		public void showSuggestion(string text, SuggestionIcon icon)
		{
			suggestionLabel.Invoke((MethodInvoker)(() =>
			{
				suggestionLabel.Text = text;
				suggestionLabel.Visible = true;
				if (icon == SuggestionIcon.Info)
				{
					suggestionIcon.Image = Properties.Resources.white_info;
				}
				else if (icon == SuggestionIcon.Check)
				{
					suggestionIcon.Image = Properties.Resources.white_check;
				}
				else if (icon == SuggestionIcon.Warning)
				{
					suggestionIcon.Image = Properties.Resources.white_warning;
				}
				else if (icon == SuggestionIcon.Slideshow)
				{
					suggestionIcon.Image = Properties.Resources.white_slideshow;
				}
				else
				{
					suggestionIcon.Image = Properties.Resources.white_fullscreen;
				}
				suggestionIcon.Height = suggestionLabel.Height;
				suggestionIcon.Visible = true;
			}));

			if (suggestionTimer != null)
			{
				suggestionTimer.Dispose();
			}
			suggestionTimer = new System.Threading.Timer((obj) =>
			{
				hideSuggestion();

			}, null, 3000, System.Threading.Timeout.Infinite);
		}

		private void hideSuggestion()
		{
			suggestionTimer.Dispose();
			suggestionLabel.Invoke((MethodInvoker)(() =>
			{
				suggestionLabel.Text = string.Empty;
				suggestionLabel.Visible = false;
				suggestionIcon.Image.Dispose();
				suggestionIcon.Visible = false;
			}));
		}

		private void slideshowButton_Click(object sender, EventArgs e)
		{
			toggleSlideshow();
		}

		private void typeOpsButton_Click(object sender, EventArgs e)
		{
			Button b = sender as Button;
			string text = b.Text.Substring(1, b.Text.Length - 1);
			string type = text.Substring(0, text.IndexOf(' ')).ToLower();

			if (type == "svg")
			{
				SvgOpsForm sof = new SvgOpsForm(Path.Combine(currentFolder, currentFile),
					pictureBox.Image.Width,
					pictureBox.Image.Height,
					picturePanel.Width - 32,
					picturePanel.Height - 32
				);
				sof.Owner = this;
				sof.DarkMode = darkMode;
				sof.ShowDialog();
			}
		}

		private void reloadButton_Click(object sender, EventArgs e)
		{
			openFile(Path.Combine(currentFolder, currentFile));
			showSuggestion(LangMan.Get("file-reloaded"), SuggestionIcon.Check);
		}

		private void newWindowButton_Click(object sender, EventArgs e)
		{
			NewWindow();
		}

		public void RestartApp()
		{
			restartApp = true;
			Close();
		}

		public void NewWindow()
		{
			Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quick-picture-viewer.exe"));
		}

		private void rotate180Button_Click(object sender, EventArgs e)
		{
			originalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
			pictureBox.Image = originalImage;
			setZoomText(LangMan.Get("auto"));
			setImageChanged(true);
		}

		private void miniViewButton_Click(object sender, EventArgs e)
		{
			Hide();

			MiniViewForm mvf = new MiniViewForm(originalImage, Text, checkboardBackground);
			mvf.Owner = this;
			mvf.Show();
		}

		private void settingsButton_Click(object sender, EventArgs e)
		{
			setSlideshow(false);

			SettingsForm settingsBox = new SettingsForm();
			settingsBox.Owner = this;
			settingsBox.TopMost = alwaysOnTop;
			settingsBox.DarkMode = darkMode;
			settingsBox.ShowDialog();
		}

		private void typeOpsButton_VisibleChanged(object sender, EventArgs e)
		{
			Console.WriteLine(typeOpsButton.Location.X);
			Console.WriteLine(ClientRectangle.Width);
			if (typeOpsButton.Visible)
			{
				typeOpsButton.Focus();
			}
		}

		private void zoomOutButton_MouseLeave(object sender, EventArgs e)
		{
			zoomOutTimer.Stop();
		}

		private void zoomOutButton_MouseUp(object sender, MouseEventArgs e)
		{
			zoomOutTimer.Stop();
		}

		private void zoomInButton_MouseLeave(object sender, EventArgs e)
		{
			zoomInTimer.Stop();
		}

		private void zoomInButton_MouseUp(object sender, MouseEventArgs e)
		{
			zoomInTimer.Stop();
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			try
			{
				CustomJumplist jumplist = new CustomJumplist(LangMan.Get("new-window"), LangMan.Get("new-window-desc"));
			}
			catch
			{

			}
		}

		private void copyFileBtn_Click(object sender, EventArgs e)
		{
			try
			{
				string[] filesToCopy = { Path.Combine(currentFolder, currentFile) };
				Clipboard.Clear();
				Clipboard.SetData(DataFormats.FileDrop, filesToCopy);
				showSuggestion(LangMan.Get("file-copied-to-clipboard"), SuggestionIcon.Check);
			}
			catch
			{
				showSuggestion(LangMan.Get("cur-file-not-found"), SuggestionIcon.Warning);
			}
		}

		private void externalRunBtn_Click(object sender, EventArgs e)
		{
			try
			{
				string ext = "bmp";
				if (currentFile != null)
				{
					ext = Path.GetExtension(currentFile).Substring(1, Path.GetExtension(currentFile).Length - 1);
				}

				Process.Start(FileAssociation.GetExecFileAssociatedToExtension(ext), Path.Combine(currentFolder, currentFile));
			}
			catch
			{
				showSuggestion(LangMan.Get("unable-to-run-external-app"), SuggestionIcon.Warning);
			}
		}

		private void externalFavoriteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(Properties.Settings.Default.FavoriteExternalApp, Path.Combine(currentFolder, currentFile));
			}
			catch
			{
				showSuggestion(LangMan.Get("unable-to-run-external-app"), SuggestionIcon.Warning);
			}
		}

		private void externalBtn_DropDownOpened(object sender, EventArgs e)
		{
			int lastSlashIndex = Properties.Settings.Default.FavoriteExternalApp.LastIndexOf('/');
			if (lastSlashIndex == -1)
				lastSlashIndex = Properties.Settings.Default.FavoriteExternalApp.LastIndexOf('\\');

			if (lastSlashIndex >= 0)
			{
				string appName = Properties.Settings.Default.FavoriteExternalApp.Substring(lastSlashIndex + 1, Properties.Settings.Default.FavoriteExternalApp.Length - lastSlashIndex - 1);
				externalFavoriteBtn.Text = LangMan.Get("open-with") + " \"" + appName + "\"";
			}
			else
			{
				externalFavoriteBtn.Text = LangMan.Get("open-with-custom");
			}
		}

		private void picturePanel_MouseEnter(object sender, EventArgs e)
		{
			picturePanel.Focus();
			AllowDrop = true;
		}

		private void backClearBtn_Click(object sender, EventArgs e)
		{
			picturePanel.BackColor = Color.Transparent;
			Properties.Settings.Default.BackColor = "";
			Properties.Settings.Default.Save();
		}

		private void backCustomBtn_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				setCheckboardBackground(false, true);
				picturePanel.BackColor = colorDialog1.Color;
				Properties.Settings.Default.BackColor = colorDialog1.Color.ToArgb().ToString();
				Properties.Settings.Default.Save();
			}
		}

		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			if (!autoZoom)
			{
				UpdatePictureBoxLocation();
			}

			if (navPanel != null && !navPanel.IsDisposed)
			{
				navPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				if (navPanel.Location.X < navPanel.borderSpacing)
				{
					navPanel.Location = new Point(navPanel.borderSpacing, navPanel.Location.Y);
				}
				if (navPanel.Location.Y < navPanel.borderSpacing + navPanel.extraTopMargin)
				{
					navPanel.Location = new Point(navPanel.Location.X, navPanel.borderSpacing + navPanel.extraTopMargin);
				}
				if (navPanel.Location.X + navPanel.Width > ClientRectangle.Width - navPanel.borderSpacing)
				{
					navPanel.Location = new Point(ClientRectangle.Width - navPanel.borderSpacing - navPanel.Width, navPanel.Location.Y);
				}
				if (navPanel.Location.Y + navPanel.Height > ClientRectangle.Height - navPanel.borderSpacing - navPanel.extraBottomMargin)
				{
					navPanel.Location = new Point(navPanel.Location.X, ClientRectangle.Height - navPanel.borderSpacing - navPanel.Height - navPanel.extraBottomMargin);
				}
			}
		}

		private void actualSizeBtn_Click(object sender, EventArgs e)
		{
			setZoomText("100%");
		}

		private void zoomTextBox_MouseEnter(object sender, EventArgs e)
		{
			zoomTextBox.Focus();
			zoomTextBox.TextBox.SelectAll();
		}

		private void zoomTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (!autoZoom) setZoomText(zoomFactor + "%");
				e.Handled = true;
			}
		}

		private void SetFramelessMode(bool b)
		{
			framelessMode = b;
			statusStrip1.SizingGrip = !b;
			framelessBtn.Checked = b;
			framelessCloseBtn.Visible = b;

			if (b)
			{
				FormBorderStyle = FormBorderStyle.None;
			}
			else if (!fullscreen)
			{
				FormBorderStyle = FormBorderStyle.Sizable;
			}
		}

		private void framelessBtn_Click(object sender, EventArgs e)
		{
			SetFramelessMode(!framelessMode);
		}

		private void effectsBtn_DropDownOpening(object sender, EventArgs eventArgs)
		{
			for (int i = effectsBtn.DropDownItems.Count - 1; i > 0; i--)
			{
				if (effectsBtn.DropDownItems[i].Image != null)
				{
					effectsBtn.DropDownItems[i].Image.Dispose();
				}
				effectsBtn.DropDownItems.Remove(effectsBtn.DropDownItems[i]);
			}

			for (int i = toolsBtn.DropDownItems.Count - 1; i > 0; i--)
			{
				if (toolsBtn.DropDownItems[i].Image != null)
				{
					toolsBtn.DropDownItems[i].Image.Dispose();
				}
				toolsBtn.DropDownItems.Remove(toolsBtn.DropDownItems[i]);
			}

			PluginMan.pluginsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
			PluginMan.apiVer = 3;
			PluginMan.inputType = "bitmap";

			PluginInfo[] plugins = PluginMan.GetPlugins(true);

			for (int i = 0; i < plugins.Length; i++)
			{
				for (int j = 0; j < plugins[i].functions.Length; j++)
				{
					string path = null;
					if (currentFile != null && currentFolder != null)
					{
						path = Path.Combine(currentFolder, currentFile);
					}
					PluginMenuItem tsmi = new PluginMenuItem(
						originalImage,
						path,
						darkMode,
						plugins[i],
						plugins[i].functions[j],
						alwaysOnTop,
						Properties.Settings.Default.Language,
						this
					);
					tsmi.Output += Tsmi_Output;

					if (plugins[i].functions[j].type == "effect")
					{
						effectsBtn.DropDownItems.Add(tsmi);
					}
					else if (plugins[i].functions[j].type == "tool")
					{
						toolsBtn.DropDownItems.Add(tsmi);
					}
				}
			}
		}

		private void Tsmi_Output(object sender, PluginMan.OutputEventArgs e)
		{
			Show();
			if (e.input != null)
			{
				directoryLabel.Visible = false;
				string title = string.Format(LangMan.Get("from-format"), sender.ToString());

				if((sender as PluginMenuItem).OwnerItem.Name == "effectsBtn")
				{
					setImageChanged(false);
				}
				openImage(e.input as Bitmap, null, title);
				setImageChanged(true, title);
			}
		}

		private void zoomTextBox_MouseLeave(object sender, EventArgs e)
		{
			picturePanel.Focus();
		}

		private void pluginManBtn_Click(object sender, EventArgs e)
		{
			PluginManForm pmf = new PluginManForm();
			pmf.Owner = this;
			pmf.TopMost = alwaysOnTop;
			pmf.DarkMode = darkMode;
			pmf.ShowDialog();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (restartApp)
			{
				NewWindow();
			}
		}

		private void MainForm_ResizeBegin(object sender, EventArgs e)
		{
			if (navPanel != null && !navPanel.IsDisposed)
			{
				navPanel.Anchor = AnchorStyles.None;
				if (navPanel.Location.X == navPanel.borderSpacing)
				{
					navPanel.Anchor |= AnchorStyles.Left;
				}
				if (navPanel.Location.Y == navPanel.extraTopMargin + navPanel.borderSpacing)
				{
					navPanel.Anchor |= AnchorStyles.Top;
				}
				if (navPanel.Location.X + navPanel.Width == ClientRectangle.Width - navPanel.borderSpacing)
				{
					navPanel.Anchor |= AnchorStyles.Right;
				}
				if (navPanel.Location.Y + navPanel.Height == ClientRectangle.Height - navPanel.borderSpacing - navPanel.extraBottomMargin)
				{
					navPanel.Anchor |= AnchorStyles.Bottom;
				}

				if (navPanel.Anchor == AnchorStyles.None)
				{
					navPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				}
			}
		}

		public void ShowNavPanel(bool showPanel, bool hideToolstripBtns)
		{
			if (showPanel)
			{
				if (navPanel == null || navPanel.IsDisposed)
				{
					int extraTopMargin = 0;
					if (toolStrip1.Visible) extraTopMargin += toolStrip1.Height;

					int extraBottomMargin = 0;
					if (statusStrip1.Visible) extraBottomMargin += statusStrip1.Height;

					navPanel = new NavPanel(extraTopMargin, extraBottomMargin);
					Controls.Add(navPanel);
					navPanel.BringToFront();
					statusStrip1.BringToFront();
				}
			}
			else
			{
				if (navPanel != null && !navPanel.IsDisposed)
				{
					navPanel.Parent.Controls.Remove(navPanel);
					navPanel.Dispose();
					navPanel = null;
				}
			}

			nextButton.Visible = !hideToolstripBtns;
			prevButton.Visible = !hideToolstripBtns;
			slideshowButton.Visible = !hideToolstripBtns;
		}

		private void framelessCloseBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void statusStrip1_VisibleChanged(object sender, EventArgs e)
		{
			if (navPanel != null && !navPanel.IsDisposed)
			{
				if (statusStrip1.Visible)
				{
					navPanel.SetExtraBottomMargin(statusStrip1.Height);
				}
				else
				{
					navPanel.SetExtraBottomMargin(0);
				}
			}

			showStatusBarBtn.Checked = statusStrip1.Visible;

			UpdatePicturePanelHeight();
			UpdatePictureBoxLocation();
		}

		private void toolStrip1_VisibleChanged(object sender, EventArgs e)
		{
			if (navPanel != null && !navPanel.IsDisposed)
			{
				if (toolStrip1.Visible)
				{
					navPanel.SetExtraTopMargin(toolStrip1.Height);
				}
				else
				{
					navPanel.SetExtraTopMargin(0);
				}
			}

			showToolbarBtn.Checked = toolStrip1.Visible;

			UpdatePicturePanelHeight();
			UpdatePictureBoxLocation();
		}

		private void showStatusBarBtn_Click(object sender, EventArgs e)
		{
			statusStrip1.Visible = !statusStrip1.Visible;
			Properties.Settings.Default.ShowStatusBar = statusStrip1.Visible;
			Properties.Settings.Default.Save();
		}

		private void showToolbarBtn_Click(object sender, EventArgs e)
		{
			toolStrip1.Visible = !toolStrip1.Visible;
			Properties.Settings.Default.ShowToolbar = toolStrip1.Visible;
			Properties.Settings.Default.Save();
		}
    }
}