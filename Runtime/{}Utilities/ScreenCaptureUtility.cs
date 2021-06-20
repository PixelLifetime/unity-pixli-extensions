using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO.Compression;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public static class ScreenCaptureUtility
{
	public delegate void FetchTexture2DAction(Texture2D texture2D);

	public static void CaptureOverlayedScreenshotAsTexture(MonoBehaviour invoker, TextureOverlayData[] texturesOverlayData, FetchTexture2DAction fetchTexture2DAction)
	{
		invoker.StartCoroutine(ScreenCaptureUtility.CaptureOverlayedScreenshotAsTextureProcess(texturesOverlayData, fetchTexture2DAction));
	}

	public static void CameraCaptureOverlayedScreenshotAsTexture(MonoBehaviour invoker, Camera camera, int width, int height, TextureOverlayData[] texturesOverlayData, FetchTexture2DAction fetchTexture2DAction)
	{
		invoker.StartCoroutine(ScreenCaptureUtility.CameraCaptureOverlayedScreenshotAsTextureProcess(camera, width, height, texturesOverlayData, fetchTexture2DAction));
	}

	public static IEnumerator CaptureOverlayedScreenshotAsTextureProcess(TextureOverlayData[] texturesOverlayData, FetchTexture2DAction fetchTexture2DAction)
	{
		yield return new WaitForEndOfFrame();

		Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

		Color[] pixels = texture.GetPixels();

		for (int i = 0; i < texturesOverlayData.Length; i++)
		{
			Color[] overlayPixels = texturesOverlayData[i]._OverlayTexture.GetPixels();

			for (int y = 0; y < texture.height && y < texturesOverlayData[i]._OverlayTexture.height; y++)
			{
				for (int x = 0; x < texture.width && x < texturesOverlayData[i]._OverlayTexture.width; x++)
				{
					if (overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x].a > 0f)
					{
						if (overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x].a >= 1f)
						{
							pixels[y * texture.width + (int)(texturesOverlayData[i]._PositionRatio.y * texture.height) * texture.width + x + (int)(texturesOverlayData[i]._PositionRatio.x * texture.width)] =
								overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x];
						}
						else
						{
							pixels[y * texture.width + (int)(texturesOverlayData[i]._PositionRatio.y * texture.height) * texture.width + x + (int)(texturesOverlayData[i]._PositionRatio.x * texture.width)] =
								pixels[y * texture.width + (int)(texturesOverlayData[i]._PositionRatio.y * texture.height) * texture.width + x + (int)(texturesOverlayData[i]._PositionRatio.x * texture.width)].NormalBlend(overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x]);
						}
					}
				}
			}
		}

		texture.SetPixels(pixels);
		texture.Apply(false, false);

		fetchTexture2DAction.Invoke(texture);
	}

	public static IEnumerator CameraCaptureOverlayedScreenshotAsTextureProcess(Camera camera, int width, int height, TextureOverlayData[] texturesOverlayData, FetchTexture2DAction fetchTexture2DAction)
	{
		yield return new WaitForEndOfFrame();

		bool initialCameraState = camera.enabled;

		Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
		texture.filterMode = FilterMode.Bilinear;

		RenderTexture renderTexture = new RenderTexture(width, height, 24);

		camera.targetTexture = renderTexture;
		camera.Render();

		RenderTexture.active = renderTexture;

		texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

		camera.targetTexture = null;
		camera.enabled = initialCameraState;

		RenderTexture.active = null;

		Color[] pixels = texture.GetPixels();

		for (int i = 0; i < texturesOverlayData.Length; i++)
		{
			Color[] overlayPixels = texturesOverlayData[i]._OverlayTexture.GetPixels();

			for (int y = 0; y < texture.height && y < texturesOverlayData[i]._OverlayTexture.height; y++)
			{
				for (int x = 0; x < texture.width && x < texturesOverlayData[i]._OverlayTexture.width; x++)
				{
					if (overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x].a > 0f)
					{
						if (overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x].a >= 1f)
						{
							pixels[y * texture.width + (int)(texturesOverlayData[i]._PositionRatio.y * texture.height) * texture.width + x + (int)(texturesOverlayData[i]._PositionRatio.x * texture.width)] =
								overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x];
						}
						else
						{
							pixels[y * texture.width + (int)(texturesOverlayData[i]._PositionRatio.y * texture.height) * texture.width + x + (int)(texturesOverlayData[i]._PositionRatio.x * texture.width)] =
								pixels[y * texture.width + (int)(texturesOverlayData[i]._PositionRatio.y * texture.height) * texture.width + x + (int)(texturesOverlayData[i]._PositionRatio.x * texture.width)].NormalBlend(overlayPixels[y * texturesOverlayData[i]._OverlayTexture.width + x]);
						}
					}
				}
			}
		}

		texture.SetPixels(pixels);
		texture.Apply(false, false);

		fetchTexture2DAction.Invoke(texture);
	}

	[System.Serializable]
	public struct TextureOverlayData
	{
		[SerializeField] private Texture2D _overlayTexture;
		public Texture2D _OverlayTexture { get { return this._overlayTexture; } }

		[SerializeField] private Vector2 _positionRatio;
		public Vector2 _PositionRatio { get { return this._positionRatio; } }
	}
}
#endif