using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // لاستعمال TextMeshProUI

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location References")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destroy the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip fireSound;

    [Header("Score System")]
    public TextMeshProUGUI scoreText; // عنصر TextMeshProUI لعرض النقاط
    private int score = 0;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // دعم Meta SDK: إطلاق النار عند الضغط على زر Trigger في اليد اليمنى
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            PullTheTrigger();
        }
    }

    public void PullTheTrigger()
    {
        gunAnimator.SetTrigger("Fire");
        Shoot();
    }

    // دالة إطلاق النار وإضافة النقاط عند إصابة الهدف
    void Shoot()
    {
        source.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            // إنشاء تأثير فوهة السلاح (Muzzle Flash)
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // تدمير التأثير بعد مدة
            Destroy(tempFlash, destroyTimer);
        }

        // إلغاء التنفيذ إذا لم يكن هناك رصاصة
        if (!bulletPrefab) { return; }

        // إنشاء الرصاصة وإطلاقها
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        // التحقق من إصابة الهدف باستخدام Raycast
        RaycastHit hit;
        if (Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hit, 100f))
        {
            string hitTag = hit.collider.tag;

            switch (hitTag)
            {
                case "X_Zone":
                    score += 50;
                    break;
                case "Nine_Zone":
                    score += 30;
                    break;
                case "Eight_Zone":
                    score += 20;
                    break;
                default:
                    score += 10; // أي جزء آخر من الهدف
                    break;
            }

            UpdateScoreUI();
        }
    }

    // دالة لتحديث واجهة المستخدم بالنقاط الجديدة
    void UpdateScoreUI()
    {
        if (scoreText)
            scoreText.text = "Score: " + score;
    }

    // دالة لإخراج الظرف الفارغ (Casing)
    void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab) { return; }

        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);

        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(
            Random.Range(ejectPower * 0.7f, ejectPower),
            (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);

        tempCasing.GetComponent<Rigidbody>().AddTorque(
            new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)),
            ForceMode.Impulse);

        Destroy(tempCasing, destroyTimer);
    }
}
