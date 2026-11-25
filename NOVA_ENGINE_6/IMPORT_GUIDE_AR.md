# دليل استيراد NOVA ENGINE 6 إلى Unity 6

> هذه التعليمات تخص حزمة المصدر المحلية v1.2.0. ملفات المصدر الكاملة لم تُرفع بعد إلى هذا المستودع.

## المتطلبات
- Unity 6 (6000.0).
- مشروع Unity جديد أو موجود.
- نسخة احتياطية من المشروع قبل إضافة الحزم.

## الاستيراد من ملف ZIP
1. فك ضغط أرشيف NOVA ENGINE 6 v1.2.0 على جهازك.
2. افتح مجلد المشروع الذي يحتوي على مجلدَي `Assets` و`Packages`.
3. انسخ مجلدات `com.nova.engine6.*` من مجلد `Packages` في الأرشيف إلى مجلد `Packages` في مشروعك.
4. افتح Unity وانتظر انتهاء Package Manager من فهرسة الحزم.
5. راجع نافذة Console؛ عالج أخطاء C# قبل تجربة أي نظام.
6. من قائمة NOVA ENGINE 6 افتح أدوات التحقق وإعداد المشروع إن ظهرت في Unity Editor.

## الاستيراد عبر Git
يمكن إضافة كل حزمة كمجلد محلي داخل مجلد `Packages`. لا تستخدم رابط هذا المستودع كمصدر UPM للحزم الكاملة حالياً، لأن هذا الفرع يحتوي على manifests ووثائق handoff ولا يحتوي بعد على ملفات Runtime/Editor الكاملة.

## بناء Android
- فعّل Android Build Support مع Android SDK & NDK Tools وOpenJDK من Unity Hub على جهاز كمبيوتر.
- من Unity افتح File > Build Profiles، اختر Android، ثم Switch Platform.
- اضبط Package Name ونسخة التطبيق وعمارة المعالج حسب احتياجك.
- اختبر المشروع على جهاز Android قبل إنشاء APK نهائي.

## حدود التحقق الحالي
- فحص GitHub Actions نجح لملفات package.json.
- هذا لا يثبت أن ملفات C# تُترجم أو أن الحزم تعمل داخل Unity.
- لم يتم إنشاء APK أو IPA.
- Unity Editor وAndroid Build Support مطلوبان للبناء؛ لا يمكن إتمام بناء Unity الرسمي من تطبيق Android وحده.
