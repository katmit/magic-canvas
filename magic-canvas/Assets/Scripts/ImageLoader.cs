using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor; // This namespace is for EditorUtility, only works in Editor

public class ImageLoader : MonoBehaviour
{
    public GameObject imagePrefab; // Assign your Image Prefab in the Inspector
    public Transform parentCanvas; // Assign your Canvas or UI Container in the Inspector

    public void OnButtonClick()
    {
        // Open a file dialog to select an image
        string imagePath = EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg");

        if (!string.IsNullOrEmpty(imagePath))
        {
            // Read the image file as a byte array
            byte[] imageData = File.ReadAllBytes(imagePath);

            // Create a new Texture2D
            Texture2D texture = new Texture2D(2, 2); // Size doesn't matter, will be resized by LoadImage
            texture.LoadImage(imageData);

            // Create a Sprite from the Texture2D
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Assign the Sprite to the target Image component
            if (imagePrefab != null && parentCanvas != null)
            {
                // Instantiate the prefab
                GameObject newImageGO = Instantiate(imagePrefab, parentCanvas);

                // Access the Image component if you need to modify its properties
                Image newImage = newImageGO.GetComponent<Image>();

                // Example: Set position and size (using RectTransform for UI elements)
                RectTransform rectTransform = newImageGO.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, 0); // Center
                rectTransform.sizeDelta = new Vector2(100, 100); // Set size

                newImage.sprite = sprite;
            }
        }
    }
}