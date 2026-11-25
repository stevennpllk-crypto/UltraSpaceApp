# NOVA ENGINE 6 — Input & Control Systems 0.8.0

حزمة إدخال وتحكم متعددة المنصات لـ Unity 6.

## المحتويات
- `NovaInputReader`: يجمع إدخال لوحة المفاتيح وإدخال واجهة اللمس الخارجي.
- `NovaCharacterMotor`: تحريك لاعب باستخدام CharacterController والقفز والجاذبية.
- `NovaVirtualJoystick`: عصا تحكم افتراضية لواجهة UI.
- `NovaTouchButton`: زر لمس لإرسال أمر القفز.
- `NovaCameraLook`: دوران الكاميرا بإدخال الفأرة، مع واجهة لإضافة حركة لمس من سكربت آخر.
- `NovaInputDiagnostics`: عرض حالة الإدخال والمنصة.

## التثبيت والإعداد
1. فك الضغط وانسخ `Packages/com.nova.engine6.input` إلى مجلد `Packages` في مشروع Unity 6.
2. أضف `CharacterController` و`NovaInputReader` و`NovaCharacterMotor` إلى اللاعب.
3. أضف Canvas وEventSystem، ثم أنشئ UI للعصا: اربط Background وHandle وNovaInputReader في `NovaVirtualJoystick`.
4. أضف `NovaTouchButton` إلى زر القفز واربط Input Reader.
5. اربط كاميرا اللاعب مع `NovaCameraLook` عند الحاجة.

## توافق الإدخال
- يستخدم هذا الإصدار واجهة Unity القديمة `UnityEngine.Input` للوحة المفاتيح والفأرة، لذلك فعّل **Input Manager (Old)** أو **Both** من إعدادات Player > Active Input Handling.
- عصا اللمس وزر اللمس يعتمدان على EventSystem وواجهة Unity UI.
- `NovaCameraLook.AddLookDelta` متاحة لربط سحب إصبع على منطقة UI عبر سكربت مخصص؛ لا توجد منطقة لمس كاميرا جاهزة في هذه النسخة.

## ملاحظات
- هذه حزمة تأسيسية وليست نظام تحكم نهائياً لكل أنواع الألعاب.
- يجب ضبط Tag اللاعب وطبقات التصادم والكاميرا حسب مشروعك.
- لم يتم تشغيل الحزمة داخل Unity في هذه البيئة؛ اختبرها قبل اعتمادها.
- Unity Editor الرسمي لا يعمل على Android؛ الاختبار والبناء يتطلبان كمبيوتر أو خدمة بناء سحابية مناسبة.
