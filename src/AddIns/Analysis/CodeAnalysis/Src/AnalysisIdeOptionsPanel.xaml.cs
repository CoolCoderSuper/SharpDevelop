﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.IO;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using Microsoft.Win32;

namespace ICSharpCode.CodeAnalysis
{
	public partial class AnalysisIdeOptionsPanel : OptionPanel
	{
		public AnalysisIdeOptionsPanel()
		{
			InitializeComponent();
			ShowStatus();
		}
		
		private void ShowStatus()
		{
			string path = FxCopWrapper.FindFxCopPath();
			if (path == null) {
				status.Text = StringParser.Parse("${res:ICSharpCode.CodeAnalysis.IdeOptions.FxCopNotFound}");
			} else {
				status.Text = $"{StringParser.Parse("${res:ICSharpCode.CodeAnalysis.IdeOptions.FxCopFoundInPath}")}{Environment.NewLine}{path}";
			}
		}
		
		private void FindFxCopPath_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.DefaultExt = "exe";
			dlg.Filter = StringParser.Parse("FxCop|fxcop.exe|${res:SharpDevelop.FileFilter.AllFiles}|*.*");
			if (dlg.ShowDialog() == true) {
				string path = Path.GetDirectoryName(dlg.FileName);
				if (FxCopWrapper.IsFxCopPath(path)) {
					FxCopPath = path;
				} else {
					MessageService.ShowError("${res:ICSharpCode.CodeAnalysis.IdeOptions.DirectoryDoesNotContainFxCop}");
				}
			}
			ShowStatus();
		}
		
		public static string FxCopPath {
			get { return PropertyService.Get("CodeAnalysis.FxCopPath", String.Empty); }
			set { PropertyService.Set("CodeAnalysis.FxCopPath", value); }
		}
	}
}
