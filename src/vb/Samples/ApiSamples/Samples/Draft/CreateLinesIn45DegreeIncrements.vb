﻿Imports SolidEdgeFramework.Extensions 'SolidEdge.Community.dll
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ApiSamples.Draft
	''' <summary>
	''' Creates a new draft and draws multiple lines in 45 degree increments.
	''' </summary>
	Friend Class CreateLinesIn45DegreeIncrements
		Private Shared Sub RunSample(ByVal breakOnStart As Boolean)
			If breakOnStart Then
				System.Diagnostics.Debugger.Break()
			End If

			Dim application As SolidEdgeFramework.Application = Nothing
			Dim documents As SolidEdgeFramework.Documents = Nothing
			Dim draftDocument As SolidEdgeDraft.DraftDocument = Nothing
			Dim sheet As SolidEdgeDraft.Sheet = Nothing
			Dim lines2d As SolidEdgeFrameworkSupport.Lines2d = Nothing
			Dim line2d As SolidEdgeFrameworkSupport.Line2d = Nothing
			Dim lineLength As Double = 3.0 ' Inches

			Try
				' Register with OLE to handle concurrency issues on the current thread.
				SolidEdgeCommunity.OleMessageFilter.Register()

				' Connect to or start Solid Edge.
				application = SolidEdgeCommunity.SolidEdgeInstall.Connect(True, True)

				' Get a reference to the documents collection.
				documents = application.Documents

				' Create a new draft document.
				draftDocument = documents.AddDraftDocument()

				' Get a reference to the active sheet.
				sheet = draftDocument.ActiveSheet

				' Get a reference to the Lines2d collection.
				lines2d = sheet.Lines2d

				' Work with angle in degrees.
				For angle As Integer = 0 To 359 Step 45
					' {x1, y1, x2, y2}
					Dim startPoint() As Double = { 0.2, 0.2 }
					Dim endPoint() As Double = { 0.3, 0.2 }

					' Add the line.
					line2d = lines2d.AddBy2Points(x1:= startPoint(0), y1:= startPoint(1), x2:= endPoint(0), y2:= endPoint(1))

					' Set the line length by converting inches to meters.
					line2d.Length = lineLength * 0.0254

					' Set the angle by converting degrees to radians.
					line2d.Angle = (Math.PI / 180) * angle
				Next angle
			Catch ex As System.Exception
				Console.WriteLine(ex.Message)
			Finally
				SolidEdgeCommunity.OleMessageFilter.Unregister()
			End Try
		End Sub
	End Class
End Namespace