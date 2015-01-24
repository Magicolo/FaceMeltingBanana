public class KeySquenceLink {

	public string keyword;
	public KeySquenceLink next;
	
	
	public KeySquenceLink(string keyword, KeySquenceLink next = null){
		this.keyword = keyword;
		this.next = next;
	}
}
