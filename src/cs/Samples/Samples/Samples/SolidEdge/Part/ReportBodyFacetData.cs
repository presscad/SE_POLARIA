﻿using ApiSamples.Samples.SolidEdge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSamples.Samples.SolidEdge.Part
{
    /// <summary>
    /// Reports body facet data of the active part.
    /// </summary>
    class ReportBodyFacetData
    {
        static void RunSample(bool breakOnStart)
        {
            if (breakOnStart) System.Diagnostics.Debugger.Break();

            SolidEdgeFramework.Application application = null;
            SolidEdgePart.PartDocument partDocument = null;
            SolidEdgePart.Models models = null;
            SolidEdgePart.Model model = null;
            SolidEdgeGeometry.Body body = null;

            try
            {
                // Register with OLE to handle concurrency issues on the current thread.
                OleMessageFilter.Register();

                // Connect to or start Solid Edge.
                application = ApplicationHelper.Connect(true);

                // Make sure user can see the GUI.
                application.Visible = true;

                // Bring Solid Edge to the foreground.
                application.Activate();

                // Get a reference to the active part document.
                partDocument = application.TryActiveDocumentAs<SolidEdgePart.PartDocument>();

                if (partDocument != null)
                {
                    models = partDocument.Models;

                    if (models.Count == 0)
                    {
                        throw new System.Exception(Resources.NoGeometryDefined);
                    }

                    model = models.Item(1);
                    body = (SolidEdgeGeometry.Body)model.Body;

                    int facetCount = 0;
                    Array points = Array.CreateInstance(typeof(double), 0);
                    object normals = Array.CreateInstance(typeof(double), 0);
                    object textureCoords = Array.CreateInstance(typeof(double), 0);
                    object styleIds = Array.CreateInstance(typeof(int), 0);
                    object faceIds = Array.CreateInstance(typeof(int), 0);

                    //Returns the number of facets and the number of points on the facets for the referenced object.
                    // If Tolerance <= 0, then data is returned from the geometry cache, and not from Parasolid.
                    body.GetFacetData(
                        Tolerance: 0.1,
                        FacetCount: out facetCount,
                        Points: ref points,
                        Normals: out normals,
                        TextureCoords: out textureCoords,
                        StyleIDs: out styleIds,
                        FaceIDs: out faceIds,
                        bHonourPrefs: false);

                    // We really need an explpanation from development on how to process the out variables.
                }
                else
                {
                    throw new System.Exception(Resources.NoActivePartDocument);
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