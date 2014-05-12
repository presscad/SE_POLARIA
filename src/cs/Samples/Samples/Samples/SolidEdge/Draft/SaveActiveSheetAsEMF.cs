﻿using ApiSamples.Samples.SolidEdge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApiSamples.Samples.SolidEdge.Draft
{
    /// <summary>
    /// Saves the active sheet of the active draft to a EMF file.
    /// </summary>
    class SaveActiveSheetAsEMF
    {
        static void RunSample(bool breakOnStart)
        {
            if (breakOnStart) System.Diagnostics.Debugger.Break();

            SolidEdgeFramework.Application application = null;
            SolidEdgeDraft.DraftDocument draftDocument = null;
            SolidEdgeDraft.Sheet sheet = null;

            try
            {
                // Register with OLE to handle concurrency issues on the current thread.
                OleMessageFilter.Register();

                // Connect to or start Solid Edge.
                application = ApplicationHelper.Connect(false);

                // Get a reference to the active draft document.
                draftDocument = application.TryActiveDocumentAs<SolidEdgeDraft.DraftDocument>();

                if (draftDocument != null)
                {
                    // Get a reference to the active sheet.
                    sheet = draftDocument.ActiveSheet;

                    SaveFileDialog dialog = new SaveFileDialog();

                    // Set a default file name
                    dialog.FileName = Path.ChangeExtension(sheet.Name, ".emf");
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    dialog.Filter = "Enhanced Metafile (*.emf)|*.emf";

                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        // Save the sheet as an EMF file.
                        sheet.SaveAsEMF(dialog.FileName);

                        Console.WriteLine("Created '{0}'", dialog.FileName);
                    }
                }
                else
                {
                    throw new System.Exception(Resources.NoActiveDraftDocument);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OleMessageFilter.Unregister();
            }
        }
    }
}