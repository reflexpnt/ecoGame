// Copyright 2022 Niantic, Inc. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Collections;

using Niantic.ARDK.AR;
using Niantic.ARDK.AR.Configuration;
using Niantic.ARDK.AR.ReferenceImage;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.Utilities.Collections;
using Niantic.ARDK.Utilities.Logging;

using UnityEngine;
using UnityEngine;

using UnityEngine.Serialization;


namespace Niantic.ARDK.Extensions
{
  public class ARImageDetectionManager
    : ARConfigChanger
  {
        
        [Serializable]
        
    private struct InspectorImage
    {
      [Tooltip("The jpeg image as a bytes TextAsset. This should be a jpg file with a .bytes file extension.")]
      [SerializeField]
      public TextAsset _imageAsBytes;

      [Tooltip("A unique name for the image, which will be the IARReferenceImage's name.")]
      [SerializeField]
      public string _name;

      [Tooltip("The width of the physical image in meters.")]
      [SerializeField]
      public float _physicalWidth;
    }
        

        [Tooltip("Images that will be added to the set of images to be detected when this is initialized.")]
    [SerializeField]
    private InspectorImage[] _images;

    /// Images that will be used in the ARSession's configuration when it is next run, if this
    /// manager is enabled.
    public IReadOnlyCollection<IARReferenceImage> RuntimeImages => _readOnlyRuntimeImages;

    private readonly HashSet<IARReferenceImage> _runtimeImages = new HashSet<IARReferenceImage>();
    private ARDKReadOnlyCollection<IARReferenceImage> _readOnlyRuntimeImages;
       

    /// Adds an image to RuntimeImages and, if this manager is enabled, request that the session be
    /// re-run.
    public void AddImage(IARReferenceImage newImage)
    {
      _runtimeImages.Add(newImage);
      if (AreFeaturesEnabled)
        RaiseConfigurationChanged();
    }

    /// Removes an image from RuntimeImages and, if this manager is enabled, request that the
    /// session be re-run.
    public void RemoveImage(IARReferenceImage badImage)
    {
      if (_runtimeImages.Remove(badImage))
      {
        if (AreFeaturesEnabled)
          RaiseConfigurationChanged();
      }
      else
      {
        ARLog._Warn("Attempting to remove an image that isn't there.");
      }
    }

    protected override void InitializeImpl()
    {
      base.InitializeImpl();

      _readOnlyRuntimeImages = _runtimeImages.AsArdkReadOnly();
            /*
                        try
                        {
                            foreach (var image in _images)
                            {
                                // Destroy collider here.
                                GameObject.FindGameObjectWithTag("markerTag");
                                    }
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Error1");
                        }

                       */
            
            if (_images != null)
            {
               // if (_images.Length >= 9)
                //{
                    // if (ciccio.GetComponent<primaVolta>().)
                    foreach (var image in _images)
                    {
                        AddImage(
                          ARReferenceImageFactory.Create
                          (
                            image._name,
                            image._imageAsBytes.bytes,
                            image._imageAsBytes.bytes.Length,
                            image._physicalWidth
                          )
                        );
                    }
                    
                  
               //     Array.Resize(ref _images, 8);
                    //togli un elemento da _images
               // }
            }
                
             
    }

    protected override void EnableFeaturesImpl()
    {
      base.EnableFeaturesImpl();

      RaiseConfigurationChanged();
    }

    protected override void DisableFeaturesImpl()
    {
      base.DisableFeaturesImpl();

      RaiseConfigurationChanged();
    }

    public override void ApplyARConfigurationChange
    (
      ARSessionChangesCollector.ARSessionRunProperties properties
    )
    {
      if (!AreFeaturesEnabled)
        return;

      if (properties.ARConfiguration is IARWorldTrackingConfiguration worldConfig)
        worldConfig.DetectionImages = _runtimeImages;
    }
  }
}
