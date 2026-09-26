using UnityEngine;
using UnityEngine.UI;

// S05_MeshRenderer
// FillBackground, FillRandom은 참고용으로 이미 완성되어 있습니다.
// 이 패턴을 참고해서 FillVerticalStripes, FillCheckerboard를 완성하세요.

public class S05_Checkerboard : MonoBehaviour
{
    [SerializeField] private int canvasWidth = 256;
    [SerializeField] private int canvasHeight = 256;
    [SerializeField] private Color backgroundColor = new Color(0f, 0f, 0f, 1f);

    [Header("무늬 실습 (줄무늬·체스판 공용)")]
    [SerializeField] private int patternSize = 16;
    [SerializeField] private Color colorA = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color colorB = new Color(0.3f, 0.5f, 0.8f, 1f);

    private Texture2D canvasTexture;
    private RawImage targetImage;

    void Start()
    {
        targetImage = GetComponent<RawImage>();

        // 1. 빈 캔버스(Texture2D) 생성
        canvasTexture = new Texture2D(canvasWidth, canvasHeight);

        // 2. 픽셀 경계를 흐리지 않게
        canvasTexture.filterMode = FilterMode.Point;

        // 3. 픽셀 채우기
        FillCheckerboard(patternSize, colorA, colorB);

        // 4. 변경 사항 반영
        canvasTexture.Apply();

        // 5. 화면에 표시
        targetImage.texture = canvasTexture;
    }

    private void FillCheckerboard(int size, Color colorA, Color colorB)
    {
        for (int x = 0; x < canvasWidth; x++)
        {
            for (int y = 0; y < canvasHeight; y++)
            {
                bool isColorA = ((x / size) + (y / size)) % 2 == 0;
                Color pixelColor = isColorA ? colorA : colorB;
                canvasTexture.SetPixel(x, y, pixelColor);
            }
        }
    }
}