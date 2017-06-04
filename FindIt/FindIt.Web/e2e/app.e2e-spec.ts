import { FindIt.WebPage } from './app.po';

describe('find-it.web App', () => {
  let page: FindIt.WebPage;

  beforeEach(() => {
    page = new FindIt.WebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
