# NOVA ENGINE 6 — Audio Systems 0.7.0

حزمة أنظمة صوتية أساسية لـ Unity 6، تضاف إلى المشروع الحالي.

## المحتويات
- `NovaAudioManager`: إدارة مستويات Master / Music / SFX، مع دعم AudioMixer اختياري.
- `NovaAudioEmitter`: تشغيل أصوات مؤثرات من قائمة Clips مع تغيير بسيط في طبقة الصوت.
- `NovaMusicPlayer`: تشغيل الموسيقى وتبديل المقاطع بتلاشي تدريجي.
- `NovaAudioZone`: تشغيل صوت محيطي عند دخول لاعب يحمل Tag باسم Player إلى منطقة Trigger.
- `NovaAudioDiagnostics`: إحصاء مصادر الصوت العاملة.
- `NovaAudioBootstrap`: رسالة بدء تشغيل.

## التثبيت
1. فك ضغط الملف.
2. انسخ `Packages/com.nova.engine6.audio` إلى مجلد `Packages` في مشروع Unity 6.
3. انتظر اكتمال استيراد السكربتات.
4. أضف المكونات المطلوبة من Inspector واربط AudioClips.
5. عند استخدام AudioMixer، أنشئ Parameters مكشوفة بالأسماء `MasterVolume` و`MusicVolume` و`SfxVolume`، أو اترك مرجع Mixer فارغاً.

## ملاحظات
- `NovaAudioZone` يحتاج Collider مع Is Trigger، ويستخدم Tag باسم `Player`.
- لتفعيل Trigger يجب أن تتوفر متطلبات الفيزياء المعتادة في Unity، ومنها وجود Rigidbody على أحد طرفي التفاعل عند الحاجة.
- هذه أنظمة تأسيسية وليست نظام صوت احترافي متكامل؛ لا تشمل spatial occlusion أو إعادة توزيع صوت متقدمة.
- لم يتم تشغيل الحزمة داخل Unity في هذه البيئة، ويجب اختبارها داخل مشروع Unity 6.
- Unity Editor الرسمي لا يعمل على Android؛ البناء والاختبار يتطلبان كمبيوتر أو خدمة بناء سحابية مناسبة.
