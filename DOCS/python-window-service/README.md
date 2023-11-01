<h1>Python Window Service Control</h1>
<br>

<h2>1️⃣ Window Service 란</h2>
<h3>
┌　　　┐　　┌　　　　　　　　┐　　┌　　　　　　　┐　　┌　　　　┐　　┌　　　　　　　　　　┐<br>
　부팅　　→　　드라이버 로딩　　→　　 서비스 로딩　　→　　로그온　　→　　응용 프로그램 로딩<br>
└　　　┘　　└　　　　　　　　┘　　└　　　　　　　┘　　└　　　　┘　　└　　　　　　　　　　┘
</h3>
<p>* 윈도우 기동 과정 단순화</p>
<br>
<p>부팅 이후부터 종료 시 까지 백그라운드에서 구동되는 응용 프로그램</p>
<p>Watchdog(와치독)과 같은 감시/복구 기능 구현 시 사용</p>
<br><br><br>
<h3>장점</h3>
<p>윈도우 로그인 여부와 상관없이 구동</p>
<p>Local System Account로 구동되어 시스템 제어 권한을 가진다.</p>
<br><br><br>
<h3>단점</h3>
<p>GUI를 가질 수 없다.</p>
<p>데스크탑과 연계가 불가하다.</p>
<p>고려해야 할 사항이 많다.</p>
<br><br><br><br><br>

<h2>2️⃣ Windows Service 일반적인 제어</h2>
<h3>1. Service 사용</h3>
<p>asdasd</p>
<br>
<h3>2. sc.exe 사용</h3>
<p>C:\Windows\System32\sc.exe</p>


<br><br><br>
<h2>🔗 Reference</h2>
<p><a href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwiwka3Dg6KCAxUDVN4KHWqVAucQFnoECAwQAQ&url=https%3A%2F%2Fcrowback.tistory.com%2F202&usg=AOvVaw1T4b3rYzjvvm_T7f7-EX2R&opi=89978449">Windows Service란 무엇인가</a></p>
<p><a href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwja5fuTk6KCAxVWaN4KHcsyCaEQFnoECAoQAQ&url=https%3A%2F%2Fcosmosnet.tistory.com%2Fentry%2FWindows-Service-3-%25EC%2584%25A4%25EC%25B9%2598%25EC%2599%2580-%25EC%25A0%259C%25EA%25B1%25B0&usg=AOvVaw0lHupWvnYe70kltxKjgKbL&opi=89978449">Windows Service 고려사항</a></p>
