# Trade_Kiwoom
# 다음 사항
- 모든 기능 테스트 필요

# 예정 사항
- 시나리오 모드, 관심 종목 모드
- 한국투자증권 연동
- TradingView Webhook 연동

# 자동 매매 서비스
❓ Problem1 : 증권사 자체 프로그램을 매일 켜야하거나 수수료가 비싸다. 
               => 키움 Katch => 매일 켜야함
               => 대신(크레온) 서버매매 => 각 방향 0.19%

❓ Problem2 : 상용 프로그램은 비용적인 것도 있지만 비율로 거래하는 것이 없다. => 개수 혹은 금액으로 거래

❓ Problem3 : 키움을 기반으로 하는 것은 많아도 대신이나 한국투자증권은 없다.

‼ Idea1 : 원하는 기능이 있는 증권사 API 기반 매매 프로그램을 만들고 이를 기반으로 다른 증권사 것도 만들자.

‼ Idea2 : 중단된 퀀트 프로그램을 지속적으로 개발하여 매매 프로그램과 연동해 사용할 수 있도록 하자.

💯 Solution : API 기반 자체 매매 프로그램 + 퀀트 프로그램

# 사용기술
- C#, Visual Studio(2019), Github

# 사용 API
- Kiwoom Api(사용중)
- kIS Open_API(예정)

# 참고 자료
- 팡규의 자동 매매 프로그램, 번개트레이더
- 기타 : GPT 4o, Claude Sonnet

# 디자인
# FORM0 : 자동실행 및 업데이트
<img width="233" alt="image" src="https://github.com/krkr5628/trade_c-/assets/75410553/5de516e9-cdf4-46d1-8b39-cbd821ea8830">

# FORM1 : 매매 확인
![image](https://github.com/krkr5628/trade_c-/assets/75410553/e6859d42-581f-4a98-b59c-17c7b2fac527)

# FORM2 : 매매 설정
![image](https://github.com/krkr5628/trade_c-/assets/75410553/89d5551f-41bd-46c9-8728-7409ed7a1696)

# FORM3 : 거래 내역
![image](https://github.com/krkr5628/trade_c-/assets/75410553/cac80cdf-28f2-44bc-921e-5110440662d9)

# FORM4 : 로그 확인
![image](https://github.com/krkr5628/trade_c-/assets/75410553/c4988a4f-5eef-4d0a-affe-83896d81095c)

# FORM5 : 인증 내역 및 업데이트 내역
![image](https://github.com/krkr5628/trade_c-/assets/75410553/dc9018ea-2d67-499d-92c5-eb43bc59320c)

# 특징
- 증권사 실시간 조건 검색을 활용하여 매수 매도 가능
- 클라우드 VM을 통해 24시간 자동 거래 가능 => API 업데이트 발생시 수동 재실행
- 특정 시간에 시작 및 종료 가능
- Telegram을 통한 매매 현황 보고
- KIS(한국투자증권)과 연계하여 거래할 수 있다.(진행중)
- 퀀트 프로그램과 연계할 수 있다.(계획중)

# 동작 순서
- 시간 표시 작동, 테이블 세팅
- 저장된 설정 사항 불러오기
- 로그인 동작
- 로그인 성공시 사용자 정보 받아오기
- 조건식, 예수금, 계좌 잔고 받아오기
- 계좌잔고를 바탕으로 TABLE에 실시간 정보 추가
- 실시간 조건 검색 시작
- MODE 따른 종목 편입
- 실시간 종목 편입 및 계좌 200ms 감시 그리고 매수 조건시간 등을 통한 매수 실시 그리고 TABLE 편입 시간 내림차순 정렬
- 보유 종목 수와 매매 횟수 등을 확인
- 매수 주문 후 매수 체결 현황 파악
- 매수 완료 후 잔고 정보 업데이트 및 세금 업데이트
- 실시간 매도 종목 편입 및 계좌 200ms 감시 그리고 조건시간 등을 통한 매도 실시
- 비율, 금액 등의 조건을 확인하여 매도 동작 수행
- 매도 주문 후 매도 체결 현황 파악
- 매도 완료 후 잔고 정보 업데이트, 예수금 업데이트, 수익 및 세금 정보 업데이트
- 중복 매수 허용할 경우 매도 완료 후 대기로 재진입하여 다시 매수 할 수 있도록 실시

# 5가지 매매 상태
- 대기 : 편입 후 아무 동작 없음 OR 중복 매수가 가능한 상태
- 호출 : AND 모드에 대한 거래로 1번 발생시 변경되는 형태
- 매수중 : 매수 주문이 들어간 상태
- 매수확인 : 매수가 완료된 상태
- 매도중 : 매도 주문이 들어간 상태
- 매도확인 : 매도가 완료된 상태

# 조건식 작동 시간
- 정규장 시간 : 09:00:00 ~ 15:30:00
- 수능과 같은 특별한 날은 개별 고려

# 기본 설정
- 계좌번호 : 현재 로그인된 계정에 대한 계좌 목록(저장 시 반드시 선택)
- 계좌번호(설정값) : 이전에 저장한 계좌번호 값
- 초기자산 : 계좌에 보유하고 있는 자금을 기준으로 수익을 계산할 초기 자산
- 종목당 매수 금액 : 종목당 1회 매수할 금액
- 종목당 매수 수량 : 종목당 1회 매수할 개수
- 종목당 매수 비율 : 종목당 1회 매수할 초기 예수금 비율
- 종목당 최대 매수 금액 : 종목당 1회 매수할 금액 제한(매수 금액-수량-비율 계산 후 최종적으로 제한)
- 최대 매수 종목 수 : 매수할 종목의 총 개수
- 최소 종목 매수가 : 매매 포착 시 가격을 기준으로 해당 값 이상 종목만 거래
- 최대 종목 매수가 :  매매 포착 시 가격을 기준으로 해당 값 이하 종목만 거래

# 추가 매수 옵션
- 최대 보유종목 수 : 계좌에서 동시에 보유할 최대 종목 수
- 당일 중복 매수 금지 : 당일 매도한 종목 매수 금지
- 시간 전 검출 매수 금지 : 매수 시간 이전에 검출된 종목 중에서 이탈 전인 종목 매수 금지(매수 시간 이후 재진입의 경우 제외)
- 보유 종목 매수 금지 : 전일보유 혹은 HTS 보유의 경우 최대 매수 종목수에 카운트되지 않으므로 해당 값들의 매수 제외

# 매수 조건식 모드(택1) => 현재 조건식 최대 2개 제한
- 매수조건 : 체크하시고 시간을 입력하셔야 실행이 됩니다.
- 지수연동 : 매수조건에 해당하는 종목의 매수를 실행할 Index 범위 값 연동
- OR : 조건식 1개 or 2개를 설정하고 조건식에 상관없이 포착된 종목에 매매 실행
- AND : 조건식 2개를 설정하고 2개의 조건식에 모두 포착된 종목만 매매 실행
- Independent : 조건식 2개를 설정하고 본계좌에 조건식 별로 각각 매매 실행
- 매도조건 : 체크하시고 시간을 입력하셔야 실행이 됩니다.

# 매도 조건식 모드
- 매수된 종목이 매도 조건식에 발생하면 그때 매도 하는 기능

# 매매방식
- 지정가 : 이벤트가 발생한 시점의 현재가에 대하여 -5호가 ~ 5호가
- 시장가 : 시장가 매매

# 매매 설정(매도 조건식 OR 실시간 시세 OR 200ms 마다 Table 검사를 통해 발생)
- 동시호가 설정(지정가) : 15:40 ~ 16:00:00
- 시간외단일가 설정(지정가) : 16:00:00 ~ 18:00:00
- 익절(%) : 해당 + 퍼센트 이익이 발생하면 매도
- 익절(원) : 해당 + 원 이익이 발생하면 매도
- 익절 TS : 해당 + TS 만큼 도달하고 보존 % 만큼 하락하면 매도
- 동시호가(빨간색) : 자동으로 시간을 파악하여 동시호가 익절 매매
- 시간 외 단일가(빨간색) : 자동으로 시간을 파악하여 지정가 익절 매매
- 손절(%) : 해당 + 퍼센트 손실이 발생하면 매도
- 손절(원) : 해당 + 원 손실이 발생하면 매도
- 동시호가(파란색) : 자동으로 시간을 파악하여 동시호가 손절 매매
- 시간 외 단일가(파란색) : 자동으로 시간을 파악하여 지정가 손절 매매

# 청산 설정(09:00:00 ~ 18:00:00)
- 기본적으로 시장가 매매
- 동시호가 설정(지정가) : 15:40 ~ 16:00:00
- 시간외단일가 설정(지정가가) : 16:00:00 ~ 18:00:00
- 청산일반 : 체크하고 시간을 설정하면 해당 시간 구간에 보유 중인 모든 종목 시장가 매도 실행
- 개별청산 : 체크하고 시간을 설정하며 청산익절 혹은 청산손절을 선택
- 청산익절 (%) : 지정된 값이상의 % 수익을 달성한 모든 종목에 대해 시장가 매도 실행  
- 청산손절 (%) : 지정된 값이하의 % 손실을 달성한 모든 종목에 대해 시장가 매도 실행 
- 지수연동 : 지수 선물 연동에 대하여 범위를 이탈하면 모든 종목 시장가 매도 실행

# 지연 설정
- 종목매수텀 : 매수 간 750ms 간격 이상 원할 경우 설정
- 미체결취소(빨간색) : 매수 진입 후 매수 중인 상태의 종목 중에서 지정된 시간(ms) 이후 미체결 취소 주문 실행
- 종목매도텀 " 매도 간 750ms 간격 이상 원할 경우 설정
- 미체결취소(파란색) : 매도 진입 후 매도 중인 상태의 종목 중에서 지정된 시간(ms) 이후 미체결 취소 주문 실행

# 지수 선물 연동(1분 마다)
- 외국인 선물 : 상한 하한 범위를 정하여 10초마다 크레온 프로그램과 통신하여 기준 이탈하면 매매 정지
 => 실제 외국인 선물 값을 1분마다 증권사에서 업데이트되며, 통신을 10초마다 진행
 => 같은 컴퓨터 내에서 관리자 권한으로 실행되어야 상호 통신 가능
- 코스피 선물 % : 상한 하한 범위를 정하여 종가 기준  이탈하면 매매 정지
- 코스닥 선물 % : 상한 하한 범위를 정하여 종가 기준  이탈하면 매매 정지
- DOW30 : 상한 하한 범위를 정하여 미국 최근일 지수 종가 기준 이탈하면 매매 정지
- S&P500 : 상한 하한 범위를 정하여 미국 최근일 지수 종가 기준 이탈하면 매매 정지
- NASDAQ100 : 상한 하한 범위를 정하여 미국 최근일 지수 종가 기준 이탈하면 매매 정지
- 외국휴무중단 : 미국 최근일 휴무였으면 매매 정지
- 외국휴무스킵 : 미국 최근일 휴무였으면 모든 지수 목록 매매 정지 기능 스킵
- #0 ~ #5 : 각종 지수 범위 설정
- #0 ~ #5 : 각종 지수 범위 설정

# TELEGRAM
- 사용자의 ID 및 토큰을 확인하여 간단한 매매 현황 전송
- 명령어 수신하여 간단한 조작 가능
- Telegram_Allow : 체크하면 실행 및 일부 필수 매매 알림에 대하여 수신 가능 및 조작 가능
- USER_ID : Telegram 사용자 ID('GetIDs Bot'으로 확인할 수 있음)
- TOKEN : Telegram BOT TOKEN('BotFather'으로 대화방을 생성하면 확인할 수 있음)
- 프로그램 조작 함수
  /HELP : 명령어 리스트
  /REBOOT : 프로그램 재실행
  /SHUTDOWN : 프로그램 종료
  /START : 조건식 시작
  /STOP : 조건식 중단
  /CLEAR : 전체 청산
  /CLEAR_PLUS : 수익 청산
  /CLEAR_MINUS : 손실 청산
  /L1 : 시스템 로그
  /L2 : 주문 로그
  /L3 : 편출입 로그
  /T1 : 편출입 차트
  /T2 : 보유 차트
  /T3 : 매매내역 차트

# KIS(개발중)
- 한국투자증권의 OPEN_API를 통해 거래
- 분할 숫자를 넣어서 예수금의 N비율만큼 거래

# TradingView Webhook(개발중)
- TradingView Webhook 기반 매수 거래

# 업데이트 및 동의사항(개발중)
- 누적 업데이트 사항
- 사용상 책임소재에 대한 동의사항
- 인증번호 확인(개발중)
