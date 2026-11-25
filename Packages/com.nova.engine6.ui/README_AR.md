# NOVA ENGINE 6 — UI & HUD Systems 0.9.0

حزمة واجهات HUD والقوائم والبيانات الأساسية لـ Unity 6.

## المحتويات
- `NovaHealth`: صحة اللاعب، الضرر، العلاج، وأحداث تغير الصحة والموت.
- `NovaHealthBar`: ربط الصحة مع Slider وتدرج لوني اختياري.
- `NovaMenuController`: فتح وإغلاق القوائم، الإيقاف المؤقت، تحميل المشاهد والخروج.
- `NovaInventory`: مخزون بسيط بعناصر ومعرّفات وكميات وسعة محددة.
- `NovaNotification`: إشعارات نصية مؤقتة.
- `NovaSettingsController`: إعدادات مستوى الصوت والجودة وملء الشاشة.
- `NovaUIBootstrap`: رسالة بدء تشغيل.

## التثبيت
1. فك الضغط وانسخ `Packages/com.nova.engine6.ui` إلى مجلد `Packages` في مشروع Unity 6.
2. أضف المكونات المطلوبة إلى GameObjects.
3. أنشئ واجهات Unity UI (Canvas) واربط عناصر Slider وText وPanel وDropdown وToggle من Inspector.
4. اربط أحداث الأزرار بدوال Open / Close / Toggle أو AddItem / RemoveItem حسب الحاجة.
5. أضف أسماء المشاهد إلى Build Profiles / Scenes In Build قبل استخدام LoadScene.

## ملاحظات وتوافق
- تستخدم الحزمة عناصر `UnityEngine.UI` القياسية. تأكد من توفر Unity UI (UGUI) في المشروع.
- `NovaNotification` يستخدم مكوّن Text التقليدي؛ يمكن استبداله لاحقاً بدعم TextMeshPro.
- `NovaHealthBar` يحدث قيمة الشريط في Update لتسهيل الاستخدام؛ يمكن تحسينه لاحقاً بالاعتماد على الأحداث فقط.
- `NovaInventory` نظام بيانات بسيط داخل الذاكرة، وليس نظام حفظ دائم.
- لم يتم تشغيل الحزمة داخل Unity في هذه البيئة؛ يلزم اختبارها داخل مشروع Unity 6.
- Unity Editor الرسمي لا يعمل على Android؛ الاختبار والبناء يتطلبان كمبيوتر أو خدمة بناء سحابية مناسبة.
