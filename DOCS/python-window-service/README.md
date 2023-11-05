<h1>Python Windows Service Control</h1>
<br>

<h2>1️⃣ Windows Service란</h2>
<p>부팅 이후부터 종료 시 까지 백그라운드에서 구동되는 응용 프로그램</p>
<p>Watchdog(와치독)과 같은 감시/복구 기능 구현 시 사용</p>
<br>
<h3>
┌　　　┐　　┌　　　　　　　　┐　　┌　　　　　　　　　┐　　┌　　　　　┐　　┌　　　　　　　　　┐<br>
　부팅　　→　　드라이버 로딩　　→　　 SCM/서비스 로딩　　→　　로그온　　→　　응용 프로그램 로딩<br>
└　　　┘　　└　　　　　　　　┘　　└　　　　　　　　　┘　　└　　　　　┘　　└　　　　　　　　　┘
</h3>
<p>&nbsp;&nbsp;&nbsp;* 윈도우 기동 과정 단순화</p>
<br><br><br>
<h3>장점</h3>
<p>윈도우 로그인 여부와 상관없이 구동</p>
<p>Local System Account로 구동되어 시스템 제어 권한 부여</p>
<br><br><br>
<h3>단점</h3>
<p>GUI ❌</p>
<p>데스크탑 연계 불가</p>
<p>다수의 고려사항</p>
<br><br><br><br><br>

<h2>2️⃣ SCP와 SCM</h2>
<img src="./SCM SCP File.jpg">
<p>&nbsp;&nbsp;&nbsp; * 기본 설치 경로: C:\Windows\System32 </p>
<p>&nbsp;&nbsp;&nbsp; * .NET / Windows SDK 설치 시 자동 포함</p>
<br><br><br>
<h3>SCP (Service Control Panel)</h3>
<p>sc.exe 등과 같은 서비스 등록, 실행 제어의 역할</p>
<p>sc command</p>
<p>SCP는 커스터마이징 등 직접 구현 가능</p>
<br><br><br>
<h3>SCM (Service Control Manager)</h3>
<p>서비스 DB의 목록을 제어함</p>
<img src="./Services DB.jpg">
<p>레지스트리: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\</p>
<p>DB 내용을 잘 알더라도, 직접 수정은 호환성 등의 문제로 권장하지 않음</p>
<br><br><br>
<h3>SCM 기능</h3>
<p>&nbsp; - 설치된 서비스 DB 관리</p>
<p>&nbsp; - 부팅 또는 사용자 요구 시 서비스 구동</p>
<p>&nbsp; - 설치된 서비스 목록 열거</p>
<p>&nbsp; - 실행중인 서비스의 현재 상태 관리</p>
<p>&nbsp; - 실행중인 서비스에게 제어 신호 송신</p>
<h4>요약: 서비스 자동 구동 및 제어 확인</h4>
<br><br><br><br><br>

<h2>3️⃣ Python에서 서비스 제어하기(PyWin32)</h2>
<h3>PyWin32</h3>
<br>
<h4>Win32 API의 기능들을 Python에서 사용하기 위한 확장</h4>
<p>&nbsp; - 마우스 컨트롤</p>
<p>&nbsp; - 화면 정보 얻기</p>
<p>&nbsp; - 시간 정보 얻기</p>
<p>&nbsp; - 사용자 정보 얻기</p>
<p>&nbsp; - 파일 관리</p>
<p>&nbsp; - 클립보드 사용</p>
<br>

<h4>모듈 다운도르</h4>

```py
pip install pywin32
```

<br>
<h4>소스 코드</h4>

```py
import win32serviceutil
import servicemanager
import win32service
import win32event
import time

class LogTestService(win32serviceutil.ServiceFramework):
    _svc_name_ = "LogTestService"
    _svc_display_name_ = "Log Test Service"
    _svc_description_ = "Advanced Programming Engaged Learning"

    log_file = open('C:/Users/sung/Desktop/고급플밍/ServiceLog.txt', 'a')
    log_count = 1

    def write_log(self, text):
        self.log_file.write("[ " + time.ctime() + " ] " + text + "\n")
        self.log_file.flush()
    
    def __init__(self, args):

        # 서비스 시작 명령
        win32serviceutil.ServiceFramework.__init__(self, args)
        self.hWaitStop = win32event.CreateEvent(None, 0, 0, None)

    def SvcStop(self):

        self.write_log("구동 중지")

        # 서비스 정지 명
        self.ReportServiceStatus(win32service.SERVICE_STOP_PENDING)
        win32event.SetEvent(self.hWaitStop)

    def SvcDoRun(self):

        self.write_log("구동 시작")

        # 동작할 함수 호출
        self.main()

    def main(self):
        
        while True:

            # 1초마다 로그 남기기
            self.write_log("구동중 " + str(self.log_count) + "번 째 로그")
            self.log_count = self.log_count + 1
            time.sleep(1)
            
            # 서비스 중지 이벤트를 확인
            if win32event.WaitForSingleObject(self.hWaitStop, 0) == win32event.WAIT_OBJECT_0:
                break

        log_file.close()  # 로그 파일 닫기

# 시작점
if __name__ == '__main__':
    win32serviceutil.HandleCommandLine(LogTestService)

```

<br>
<h4>Python Script를 통한 서비스 제어</h4>

```py
# 서비스 등록
python main.py install
# 서비스 구동
python main.py start
# 서비스 중지
python main.py stop
# 서비스 재구동
python main.py restart
# 서비스 삭제
python main.py remove
```

<br>
<h4>win32serviceutil 모듈을 통한 서비스 제어</h4>

```py
# 서비스 구동
win32serviceutil.StartService(서비스 이름)
# 서비스 중지
win32serviceutil.StopService(서비스 이름)
# 서비스 재구동
win32serviceutil.RestartService(서비스 이름)
```

<br>
<h4>서비스로 등록할 클래스 내에 __init__, SvcStop, SvcDoRun 함수가 있어야 정상 동작함</h4>
<p>&nbsp; - 서비스 구동, 중지 커멘드가 실행되지 않음</p>
<br><br><br><br><br>

<h2>4️⃣ Python에서 서비스 제어하기(기타 방법)</h2>
<h3>PyInstaller 모듈로 실행파일(.exe) 생성</h3>
<h4>생성된 실행파일을 sc.exe, NSSM 등의 SCP를 통하여 서비스 등록함</h4>

<br><br><br><br><br>

<h2>🔗 Reference</h2>
<p><a href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwiwka3Dg6KCAxUDVN4KHWqVAucQFnoECAwQAQ&url=https%3A%2F%2Fcrowback.tistory.com%2F202&usg=AOvVaw1T4b3rYzjvvm_T7f7-EX2R&opi=89978449">Windows Service란 무엇인가</a></p>
<p><a href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwja5fuTk6KCAxVWaN4KHcsyCaEQFnoECAoQAQ&url=https%3A%2F%2Fcosmosnet.tistory.com%2Fentry%2FWindows-Service-3-%25EC%2584%25A4%25EC%25B9%2598%25EC%2599%2580-%25EC%25A0%259C%25EA%25B1%25B0&usg=AOvVaw0lHupWvnYe70kltxKjgKbL&opi=89978449">Windows Service 고려사항</a></p>
